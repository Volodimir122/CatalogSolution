using CatalogServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogServer.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Электроника" },
            new Category { Id = 2, Name = "Книги" },
            new Category { Id = 3, Name = "Одежда" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Смартфон", Price = 29999m, CategoryId = 1 },
            new Product { Id = 2, Name = "Ноутбук", Price = 74999m, CategoryId = 1 },
            new Product { Id = 3, Name = "Мастер и Маргарита", Price = 899m, CategoryId = 2 }
        );
    }
}