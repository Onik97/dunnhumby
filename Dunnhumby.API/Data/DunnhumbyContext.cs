namespace Dunnhumby.API.Data;

using Dunnhumby.API.Models;
using Microsoft.EntityFrameworkCore;

public class DunnhumbyContext : DbContext
{
    public DunnhumbyContext(DbContextOptions<DunnhumbyContext> options) : base(options)
    {
        Database.EnsureCreated(); // For SQlite, normally databases are created beforehand
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasIndex(p => p.ProductCode).IsUnique();
    }
}
