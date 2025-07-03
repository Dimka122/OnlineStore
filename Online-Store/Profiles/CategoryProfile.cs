using AutoMapper;
using Online_Store.Models.DTOs;
using Online_Store.Models.Entities;

namespace Online_Store.Profiles
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            // Category -> CategoryDTO
            CreateMap<Category, CategoryDTO>()
                .ForMember(dest => dest.ProductCount,
                          opt => opt.MapFrom(src => src.Products.Count));

            // Category -> CategoryDetailsDTO
            CreateMap<Category, CategoryDetailsDTO>();

            // Product -> ProductShortDTO
            CreateMap<Product, ProductShortDTO>();

            // CategoryCreateDTO -> Category
            CreateMap<CategoryCreateDTO, Category>();

            // CategoryUpdateDTO -> Category
            CreateMap<CategoryUpdateDTO, Category>();
        }
    }
}
