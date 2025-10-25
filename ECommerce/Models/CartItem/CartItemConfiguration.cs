using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class CartItemConfiguration :IEntityTypeConfiguration<CartItem>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems", "Sales");
            builder.HasKey(ci => ci.Id);
            builder.Property(ci => ci.Quantity)
                   .IsRequired();
            // Relationships
            builder.HasOne(ci => ci.User)
                   .WithMany(u => u.CartItems)
                   .HasForeignKey(ci => ci.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(ci => ci.Product)
                   .WithMany(p => p.CartItems)
                   .HasForeignKey(ci => ci.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(p => p.IsDeleted == false);

        }
    }
}
