using AutoMapper;
using ECommerce.Core.DTO;
using ECommerce.Core.Entities.Product;

namespace ECommerce.API.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductDTO>().ForMember(x => x.CategoryName, x => x.MapFrom(x => x.Category.Name)).ReverseMap();
               
            CreateMap<UpdateProductDTO, Product>().ReverseMap();
            CreateMap<Photo, PhotoDTO>().ReverseMap();
        }
    }
}
