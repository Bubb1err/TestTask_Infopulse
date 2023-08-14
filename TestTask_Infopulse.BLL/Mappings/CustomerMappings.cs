using AutoMapper;
using TestTask_Infopulse.BLL.ViewModels;
using TestTask_Infopulse.DataAccess.Entities;

namespace TestTask_Infopulse.BLL.Mappings
{
    internal class CustomerMappings : Profile
    {
        public CustomerMappings()
        {
            CreateMap<Customer, CustomerDTO>();
            CreateMap<CreateCustomerDTO, Customer>();
            CreateMap<Customer, SelectListCustomerDTO>();
        }
    }
}
