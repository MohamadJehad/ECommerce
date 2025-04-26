using ECommerce.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Data.Config
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Id).IsRequired();
            builder.HasData(
                new Category { Id=1, Description="Description for first category", Name="First Category"},
                new Category { Id=2, Description="Description for Seconed category", Name= "Seconed Category" },
                new Category { Id=3, Description="Description for Third category", Name= "Third Category" }
                );;
        }
    }
}
