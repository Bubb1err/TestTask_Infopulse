

using MediatR;
using TestTask_Infopulse.BLL.ViewModels;

namespace TestTask_Infopulse.BLL.Queries.OrderQueries
{
    public class GetAllOrdersQuery : IRequest<List<OrderDTO>>
    {
    }
}
