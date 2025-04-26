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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(30);
            builder.Property(x => x.NewPrice).IsRequired().HasColumnType("decimal(18,2)");
            builder.HasData(
                new Product { Id = 1, Name ="First Product", CategoryId = 1, NewPrice = 11, Description= "new D"},
                new Product { Id = 2, Name = "Seconed Product", CategoryId = 2, NewPrice = 22, Description = "Descrioption" },
                new Product { Id = 3, Name = "Third Product", CategoryId = 3, NewPrice = 44, Description = "Hello describe" }
                );
        }
    }
}
