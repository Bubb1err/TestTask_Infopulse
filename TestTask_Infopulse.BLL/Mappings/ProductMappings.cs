using AutoMapper;
using TestTask_Infopulse.BLL.ViewModels;
using TestTask_Infopulse.DataAccess.Entities;
using TestTask_Infopulse.DataAccess.Entities.Enums;

namespace TestTask_Infopulse.BLL.Mappings
{
    internal class ProductMappings : Profile
    {
        public ProductMappings()
        {
            CreateMap<Product, ProductDTO>().ForMember(p => p.ProductCategory, 
                m => m.MapFrom(p => p.Category))
                .ForMember(p => p.ProductSize,
                m => m.MapFrom(p => Enum.GetName<ProductSize>(p.ProductSize)));
            CreateMap<CreateProductDTO, Product>();
            CreateMap<ProductCategory, ProductCategoryDTO>().ReverseMap();
        }
    }
}
