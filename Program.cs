using CamelApp.API.Data;
using CamelApp.API.Models;
using CamelApp.API.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=camels.db"));
builder.Services.AddValidatorsFromAssemblyContaining<CamelValidator>();

var app = builder.Build();
if (app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));

using (var scope = app.Services.CreateScope()){
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DatabaseInitializer.Initialize(db);
}

app.MapGet("/api/camels", async (AppDbContext db) =>{
    try{
        var camels = await db.Camels.ToListAsync();
        return Results.Ok(camels);
    }
    catch{return Results.Problem("Adatbázis hiba");}
});

app.MapGet("/api/camels/{id}", async (int id, AppDbContext db) =>{
    try{
        var camel = await db.Camels.FindAsync(id);
        return camel != null ? Results.Ok(camel) : Results.NotFound(new { error = "404 Not Found", id });
    }
    catch{return Results.Problem("Adatbázis hiba");}
});

app.MapPost("/api/camels", async (
    Camel camel,
    AppDbContext db,
    IValidator<Camel> validator) => {
    try{
        var result = await validator.ValidateAsync(camel);
        if (!result.IsValid) return Results.ValidationProblem(result.ToDictionary());

        camel.CreatedAt = DateTime.UtcNow;
        camel.UpdatedAt = DateTime.UtcNow;

        db.Camels.Add(camel);
        await db.SaveChangesAsync();

          return Results.Created($"/api/camels/{camel.Id}", camel);
    }
    catch{return Results.Problem("Adatbázis hiba");}
});

app.MapDelete("/api/camels/{id}", async (int id, AppDbContext db) =>{
    try{
        var camel = await db.Camels.FindAsync(id);
        if (camel == null) return Results.NotFound(new { error = "404 Not Found", id });

        db.Camels.Remove(camel);
        await db.SaveChangesAsync();

        return Results.Ok(new { message = "Teve törölve" });
    }
    catch{return Results.Problem("Adatbázis hiba");}
});

app.MapPut("/api/camels/{id}", async (
    int id,
    Camel updatedCamel,
    AppDbContext db,
    IValidator<Camel> validator) =>{
    try{
        var camel = await db.Camels.FindAsync(id);
        if (camel == null) return Results.NotFound(new { error = "404 Not Found", id });

        var result = await validator.ValidateAsync(updatedCamel);
        if (!result.IsValid) return Results.ValidationProblem(result.ToDictionary());

        camel.Name = updatedCamel.Name;
        camel.Color = updatedCamel.Color;
        camel.HumpCount = updatedCamel.HumpCount;
        camel.LastFed = updatedCamel.LastFed;
        camel.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();

        return Results.Ok(camel);
    }
    catch{return Results.Problem("Adatbázis hiba");}
});

app.MapPatch("/api/camels/{id}", async (
    int id,
    Camel partialCamel,
    AppDbContext db,
    IValidator<Camel> validator) => {
    try{
        var camel = await db.Camels.FindAsync(id);
        if (camel == null) return Results.NotFound(new { error = "404 Not Found", id });
        if (!string.IsNullOrEmpty(partialCamel.Name)) camel.Name = partialCamel.Name;
        if (partialCamel.Color != null) camel.Color = partialCamel.Color;
        if (partialCamel.HumpCount > 0) camel.HumpCount = partialCamel.HumpCount;
        if (partialCamel.LastFed.HasValue) camel.LastFed = partialCamel.LastFed;

        camel.UpdatedAt = DateTime.UtcNow;

        var result = await validator.ValidateAsync(camel);
        if (!result.IsValid) return Results.ValidationProblem(result.ToDictionary());

        await db.SaveChangesAsync();
        return Results.Ok(camel);
    }
    catch{return Results.Problem("Adatbázis hiba");}
});

var port = Environment.GetEnvironmentVariable("PORT") ?? "5190";
app.Run($"http://*:{port}");
