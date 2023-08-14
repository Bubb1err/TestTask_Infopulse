using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestTask_Infopulse.BLL.CustomExceptions;
using TestTask_Infopulse.BLL.Queries.OrderQueries;
using TestTask_Infopulse.BLL.Services.LoggerService;
using TestTask_Infopulse.BLL.ViewModels;
using TestTask_Infopulse.DataAccess.Entities;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces.Custom;

namespace TestTask_Infopulse.BLL.Handlers.OrderHandlers
{
    internal class GetOrderHandler : IRequestHandler<GetOrderQuery, OrderDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _loggerManager;
        private readonly IOrdersRepository _ordersRepository;

        public GetOrderHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILoggerManager loggerManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _loggerManager = loggerManager;
            _ordersRepository = _unitOfWork.GetRepository<Order, IOrdersRepository>();
        }
        public async Task<OrderDTO> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _ordersRepository.GetFirstOrDefaultAsync(o => o.Id == request.OrderId, track: false, include: 
                source => source.Include(s => s.Customer));
            if (order == null)
            {
                _loggerManager.LogError($"Error getting the order. Order with id {request.OrderId} was not found.");
                throw new DataProcessingException(System.Net.HttpStatusCode.NotFound,
                    $"Order with id {request.OrderId} was not found.");
            }
            var orderDto = _mapper.Map<OrderDTO>(order);
            return orderDto;
        }
    }
}
