using AutoMapper;
using ECommerce.Core.DTO;
using ECommerce.Core.Entities.Product;

namespace ECommerce.API.Mapping
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<UpdateCategoryDTO, Category>().ReverseMap();
        }
    }
}
