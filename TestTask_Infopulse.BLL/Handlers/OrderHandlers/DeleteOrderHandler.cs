using MediatR;
using TestTask_Infopulse.BLL.Commands.OrderCommands;
using TestTask_Infopulse.BLL.Services.LoggerService;
using TestTask_Infopulse.DataAccess.Entities;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces.Custom;

namespace TestTask_Infopulse.BLL.Handlers.OrderHandlers
{
    internal class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _loggerManager;
        private readonly IOrdersRepository _ordersRepository;
        public DeleteOrderHandler(
            IUnitOfWork unitOfWork,
            ILoggerManager loggerManager)
        {
            _unitOfWork = unitOfWork;
            _loggerManager = loggerManager;
            _ordersRepository = _unitOfWork.GetRepository<Order, IOrdersRepository>();
        }
        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _ordersRepository.GetFirstOrDefaultAsync(o => o.Id == request.OrderId);
            if (order == null)
            {
                return Unit.Value;
            }
            _ordersRepository.Delete(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _loggerManager.LogInfo($"Order with id {order.Id} was deleted.");

            return Unit.Value;
        }
    }
}
