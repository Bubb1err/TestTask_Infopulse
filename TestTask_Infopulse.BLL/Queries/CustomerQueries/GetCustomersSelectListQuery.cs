using MediatR;
using TestTask_Infopulse.BLL.ViewModels;

namespace TestTask_Infopulse.BLL.Queries.CustomerQueries
{
    public class GetCustomersSelectListQuery : IRequest<List<SelectListCustomerDTO>>
    {
    }
}
