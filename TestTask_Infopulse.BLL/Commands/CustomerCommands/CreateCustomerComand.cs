using MediatR;
using TestTask_Infopulse.BLL.ViewModels;

namespace TestTask_Infopulse.BLL.Commands.CustomerCommands
{
    public class CreateCustomerComand : IRequest<Unit>
    {
        public CreateCustomerDTO CreateCustomerDTO { get; private set; }
        public CreateCustomerComand(CreateCustomerDTO createCustomerDTO)
        {
            CreateCustomerDTO = createCustomerDTO;
        }
    }
}
