using Ecommerce.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<CategoryProduct>().HasKey(cp => new { cp.IdCategory, cp.IdProduct });

            //modelBuilder.Entity<CategoryProduct>().HasOne(cp => cp.Product).WithMany(p => p.CategoriesProducts).HasForeignKey(cp => cp.IdProduct);

            //modelBuilder.Entity<CategoryProduct>().HasOne(cp => cp.Category).WithMany(c => c.CategoriesProducts).HasForeignKey(cp => cp.IdCategory);
        }
    }
}
