using Microsoft.EntityFrameworkCore;

namespace CamelApp.API.Data;

public static class DatabaseInitializer{
    public static void Initialize(AppDbContext context){
        context.Database.EnsureCreated();
        if (!context.Camels.Any()){
            context.Camels.AddRange(
                new Models.Camel{
                    Name = "Daryl",
                    Color = "Brown",
                    HumpCount = 1,
                    LastFed = DateTime.UtcNow.AddHours(-2),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Models.Camel{
                    Name = "Rick",
                    Color = "Black",
                    HumpCount = 2,
                    LastFed = DateTime.UtcNow.AddHours(-1),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Models.Camel{
                    Name = "Carl",
                    Color = "Blue",
                    HumpCount = 2,
                    LastFed = DateTime.UtcNow.AddHours(-2),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );
            context.SaveChanges();
        }
    }
}