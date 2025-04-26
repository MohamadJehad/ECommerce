using AutoMapper;
using ECommerce.Core.DTO;
using ECommerce.Core.Entities.Product;
using ECommerce.Core.Interfaces;
using ECommerce.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        public ProductRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
        }

        public async Task<bool> AddAsync(AddProductDTO productDTO)
        {
            if (productDTO == null)
            {
                return false;
            }

            var product = mapper.Map<Product>(productDTO);
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
