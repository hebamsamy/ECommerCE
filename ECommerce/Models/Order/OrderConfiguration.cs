using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order", "Sales");
            builder.HasKey(o => o.Id);
            //User has many Orders

            builder.HasOne(u => u.User)
                   .WithMany(o => o.Orders)
                   .HasForeignKey(o => o.UserId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.OwnsOne(o => o.ShippingAddress, o =>
            {
                o.Property(p => p.Street).HasMaxLength(100);
                o.Property(p => p.City).HasMaxLength(100);
                o.Property(p => p.Notes).HasMaxLength(200);
                o.Property(p => p.ZIPCode).HasMaxLength(10);
            });

            builder.Property(o => o.Status)
                .HasConversion<string>();

            builder.HasQueryFilter(p => p.IsDeleted == false);

            //builder.Property<DateTime>().HasColumnName("ModifiedDate");
        }
    }
}
