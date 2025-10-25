using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Models
{
    public class SupplierConfigration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> modelBuilder)
        {
            modelBuilder.ToTable("Suppliers", "Security");
            modelBuilder.HasKey(i => i.UserId);

            modelBuilder.Property(i => i.ShopName).IsRequired(false);
            modelBuilder.Property(i => i.ShopLogo).IsRequired(false);
            modelBuilder.Property(i => i.ShopLocation).IsRequired(false);

            modelBuilder
                .HasOne(v => v.User)
                .WithOne(u => u.Supplier)
                .HasForeignKey<Supplier>(v => v.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
