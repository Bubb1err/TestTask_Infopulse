using AutoMapper;
using TestTask_Infopulse.BLL.ViewModels;
using TestTask_Infopulse.DataAccess.Entities;

namespace TestTask_Infopulse.BLL.Mappings
{
    internal class ProductMappings : Profile
    {
        public ProductMappings()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<CreateProductDTO, Product>();
            CreateMap<ProductCategory, ProductCategoryDTO>().ReverseMap();
        }
    }
}
