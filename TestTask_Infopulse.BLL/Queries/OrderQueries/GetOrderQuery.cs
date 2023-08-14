using MediatR;
using TestTask_Infopulse.BLL.ViewModels;

namespace TestTask_Infopulse.BLL.Queries.OrderQueries
{
    public class GetOrderQuery : IRequest<OrderDTO>
    {
        public int OrderId { get; private set; }
        public GetOrderQuery(int orderId)
        {
            OrderId = orderId;
        }
    }
}
