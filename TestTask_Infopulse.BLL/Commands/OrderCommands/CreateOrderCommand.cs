using MediatR;
using TestTask_Infopulse.BLL.ViewModels;

namespace TestTask_Infopulse.BLL.Commands.OrderCommands
{
    public class CreateOrderCommand : IRequest<Unit>
    {
        public CreateOrderDTO CreateOrderDTO { get; private set; }
        public CreateOrderCommand(CreateOrderDTO createOrderDTO)
        {
            CreateOrderDTO = createOrderDTO;
        }
    }
}
