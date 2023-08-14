using MediatR;

namespace TestTask_Infopulse.BLL.Commands.OrderCommands
{
    public class DeleteOrderCommand : IRequest<Unit>
    {
        public int OrderId { get; private set; }
        public DeleteOrderCommand(int id)
        {
            OrderId = id;
        }
    }
}
