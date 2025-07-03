using AutoMapper;
using Online_Store.Models.DTOs;
using Online_Store.Models.Entities;

namespace Online_Store.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Product -> ProductDTO
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName,
                          opt => opt.MapFrom(src => src.Category.Name));

            // ProductCreateDTO -> Product
            CreateMap<ProductCreateDTO, Product>()
                .ForMember(dest => dest.CreatedDate,
                          opt => opt.MapFrom(_ => DateTime.UtcNow));

            // ProductUpdateDTO -> Product
            CreateMap<ProductUpdateDTO, Product>()
                .ForMember(dest => dest.CreatedDate,
                          opt => opt.Ignore());
        }
    }

}
