using Microsoft.EntityFrameworkCore;
using CamelApp.API.Models;

namespace CamelApp.API.Data;

public class AppDbContext : DbContext {

    public DbSet<Camel> Camels { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Camel>()
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);
            
        modelBuilder.Entity<Camel>()
            .Property(c => c.Color)
            .HasMaxLength(50);
    }
}