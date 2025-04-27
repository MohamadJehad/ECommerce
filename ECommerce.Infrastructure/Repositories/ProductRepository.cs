using AutoMapper;
using ECommerce.Core.DTO;
using ECommerce.Core.Entities.Product;
using ECommerce.Core.Interfaces;
using ECommerce.Core.Services;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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
        private readonly IImageManagementService imageManagementService;
        public ProductRepository(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.imageManagementService = imageManagementService;
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

            var imagePath = await this.imageManagementService.AddImageAsync(productDTO.Photos, productDTO.Name);

            var photos = imagePath.Select(x => new Photo
            {
                ImageName = x,
                ProductId = product.Id,
            }).ToList();
            await context.Photos.AddRangeAsync(photos);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAsync(UpdateProductDTO productDTO)
        {
            if (productDTO == null)
            {
                return false;
            }

            var product = await context.Products
                .Include(x => x.Photos)
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == productDTO.Id);

            if (product == null)
            {
                return false;
            }


            product = this.mapper.Map<Product>(productDTO);
            await context.SaveChangesAsync();

            var images = await context.Photos.Where(x => x.ProductId == productDTO.Id).ToListAsync();

            foreach (var photo in images)
            {
                imageManagementService.DeleteImage(photo!.ImageName);
            }

            context.Photos.RemoveRange(images);

            var imagePaths = await imageManagementService.AddImageAsync(productDTO.Photos, productDTO.Name);
            var newimages = imagePaths.Select(x => new Photo
            {
                ImageName = x,
                ProductId = productDTO.Id,
            }).ToList();

            await context.Photos.AddRangeAsync(newimages);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
