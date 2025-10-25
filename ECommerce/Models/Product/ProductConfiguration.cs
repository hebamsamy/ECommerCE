using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product", "Production");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();

            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Supplier)
                   .WithMany(s => s.Products)
                   .HasForeignKey(p => p.SupplierId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasQueryFilter(p => p.IsDeleted == false);

            //builder.HasMany(p => p.Users).WithMany(u => u.Products);

        }
    }
}
