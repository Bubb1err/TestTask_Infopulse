using MediatR;
using TestTask_Infopulse.BLL.ViewModels;

namespace TestTask_Infopulse.BLL.Commands.ProductCommands
{
    public class UpdateProductCommand : IRequest<Unit>
    {
        public EditProductDTO EditProductDTO { get; private set; }
        public UpdateProductCommand(EditProductDTO editProductDTO)
        {
            EditProductDTO = editProductDTO;
        }
    }
}
