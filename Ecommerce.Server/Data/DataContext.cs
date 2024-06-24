using Ecommerce.Server.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Server.Data;

public class DataContext : IdentityDbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<CategoryProduct> CategoryProducts { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(10, 2);

        modelBuilder.Entity<CategoryProduct>().HasKey(cp => new { cp.IdCategory, cp.IdProduct });

        modelBuilder.Entity<Product>()
            .HasMany(p => p.CategoryProducts)
            .WithOne(cp => cp.Product)
            .HasForeignKey(cp => cp.IdProduct);

        modelBuilder.Entity<Category>()
            .HasMany(c => c.CategoryProducts)
            .WithOne(cp => cp.Category)
            .HasForeignKey(cp => cp.IdCategory);

        modelBuilder.Entity<OrderProduct>()
            .HasKey(op => new { op.IdProduct, op.IdOrder });

        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Product)
            .WithMany(p => p.OrderProducts)
            .HasForeignKey(op => op.IdProduct);

        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Order)
            .WithMany(p => p.OrderProducts)
            .HasForeignKey(op => op.IdOrder);

        modelBuilder.Entity<OrderProduct>()
            .Property(op => op.Quantity)
            .IsRequired();
    }
}