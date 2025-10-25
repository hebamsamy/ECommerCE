using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class EcommerceContext: IdentityDbContext<User>
    {

        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options)
        {
        }
        //tables
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        // configuration

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseLazyLoadingProxies().UseSqlServer("");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API configurations can be added here if needed
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new CartItemConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfigration());
            base.OnModelCreating(modelBuilder);





            // Data Seeding
            modelBuilder.SeedCategory();
            modelBuilder.SeedRole();
            //modelBuilder.SeedSupplier();    
            //modelBuilder.SeedProduct();

        }









    }
}
