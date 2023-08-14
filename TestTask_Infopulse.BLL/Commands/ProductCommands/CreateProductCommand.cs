using MediatR;
using TestTask_Infopulse.BLL.ViewModels;

namespace TestTask_Infopulse.BLL.Commands.ProductCommands
{
    public class CreateProductCommand : IRequest<Unit>
    {
        public CreateProductDTO CreateProductDTO { get; private set; }
        public CreateProductCommand(CreateProductDTO createProductDTO)
        {
            CreateProductDTO = createProductDTO;
        }
    }
}
