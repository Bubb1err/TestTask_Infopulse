using AutoMapper;
using TestTask_Infopulse.BLL.ViewModels;
using TestTask_Infopulse.DataAccess.Entities;
using TestTask_Infopulse.DataAccess.Entities.Enums;

namespace TestTask_Infopulse.BLL.Mappings
{
    public class OrderMappings : Profile
    {
        public OrderMappings()
        {
            CreateMap<Order, OrderDTO>() 
                .ForMember(o => o.CustomerName,
                m => m.MapFrom(o => o.Customer.CustomerName))
                .ForMember(o => o.CustomerAddress,
                m => m.MapFrom(o => o.Customer.Address))
                .ForMember(o => o.Status,
                m => m.MapFrom(o => Enum.GetName<Status>((Status)o.Status)));
            CreateMap<CreateOrderDTO, OrderDTO>();
        }
    }
}
