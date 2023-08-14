using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestTask_Infopulse.BLL.Queries.OrderQueries;
using TestTask_Infopulse.BLL.ViewModels;
using TestTask_Infopulse.DataAccess.Entities;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces.Custom;

namespace TestTask_Infopulse.BLL.Handlers.OrderHandlers
{
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, List<OrderDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IMapper _mapper;
        public GetAllOrdersHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _ordersRepository = _unitOfWork.GetRepository<Order, IOrdersRepository>();
        }
        public async Task<List<OrderDTO>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _ordersRepository.GetAll(include: source => 
                    source.Include(s => s.Customer)).ToListAsync();

            var ordersDto = _mapper.Map<List<OrderDTO>>(orders);
            return ordersDto;
        }
    }
}
