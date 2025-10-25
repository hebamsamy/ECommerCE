using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> t)
        {
            t.ToTable("Category", "Production");
            t.HasKey(c => c.Id);
            t.Property(c => c.Name).HasMaxLength(100).IsRequired();
         
            t.Property(c => c.Description).HasMaxLength(500);
            t.HasQueryFilter(p => p.IsDeleted == false);


        }
    }
}

