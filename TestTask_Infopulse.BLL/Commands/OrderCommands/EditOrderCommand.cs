using MediatR;
using TestTask_Infopulse.BLL.ViewModels;

namespace TestTask_Infopulse.BLL.Commands.OrderCommands
{
    public class EditOrderCommand : IRequest<Unit>
    {
        public EditOrderDTO EditOrderDTO { get; private set; }
        public EditOrderCommand(EditOrderDTO editOrderDTO)
        {
            EditOrderDTO = editOrderDTO;
        }
    }
}
