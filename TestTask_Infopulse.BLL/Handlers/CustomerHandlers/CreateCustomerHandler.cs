using AutoMapper;
using MediatR;
using TestTask_Infopulse.BLL.Commands.CustomerCommands;
using TestTask_Infopulse.BLL.Services.LoggerService;
using TestTask_Infopulse.DataAccess.Entities;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces.Custom;

namespace TestTask_Infopulse.BLL.Handlers.CustomerHandlers
{
    internal class CreateCustomerHandler : IRequestHandler<CreateCustomerComand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICustomersRepository _customersRepository;
        private readonly ILoggerManager _loggerManager;

        public CreateCustomerHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILoggerManager loggerManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _loggerManager = loggerManager;
            _customersRepository = _unitOfWork.GetRepository<Customer, ICustomersRepository>();
        }
        public async Task<Unit> Handle(CreateCustomerComand request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(request.CreateCustomerDTO);
            await _customersRepository.CreateAsync(customer);
            _loggerManager.LogInfo("Creating customer.");
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
