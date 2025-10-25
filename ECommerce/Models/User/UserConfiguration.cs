using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.CreatedAt).HasDefaultValueSql("GetDate()");
            builder.Property(u => u.FullName).HasMaxLength(100).IsRequired();
            builder.OwnsOne(o => o.Address, o =>
            {
                o.Property(p => p.Street).HasMaxLength(100).IsRequired(false);
                o.Property(p => p.City).HasMaxLength(100).IsRequired(false); ;
                o.Property(p => p.Notes).HasMaxLength(200).IsRequired(false); ;
                o.Property(p => p.ZIPCode).HasMaxLength(10).IsRequired(false); ;
            });
        }
    }
}
