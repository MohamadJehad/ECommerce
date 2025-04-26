using AutoMapper;
using ECommerce.Core.DTO;
using ECommerce.Core.Entities.Product;

namespace ECommerce.API.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<ProductDTO, Product>().ReverseMap();
            CreateMap<UpdateProductDTO, Product>().ReverseMap();
        }
    }
}
