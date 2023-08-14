using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestTask_Infopulse.BLL.Queries.CustomerQueries;
using TestTask_Infopulse.BLL.ViewModels;
using TestTask_Infopulse.DataAccess.Entities;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces.Custom;

namespace TestTask_Infopulse.BLL.Handlers.CustomerHandlers
{
    internal class GetCustomersHandler : IRequestHandler<GetCustomersQuery, List<CustomerDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomersRepository _customersRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IMapper _mapper;
        public GetCustomersHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _customersRepository = _unitOfWork.GetRepository<Customer, ICustomersRepository>();
            _ordersRepository = _unitOfWork.GetRepository<Order, IOrdersRepository>();
        }
        public async Task<List<CustomerDTO>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _customersRepository.GetAll().ToListAsync();
            var customersDto = _mapper.Map<List<CustomerDTO>>(customers);
            foreach (var customer in customersDto)
            {
                decimal sum = 0;
                int count = 0;
                var orders = await _ordersRepository.GetAll(o => o.Customer.Id == customer.Id).ToListAsync();
                orders.ForEach(order =>
                {
                    sum += order.TotalCost;
                    count++;
                });
                customer.TotalOrderedCost = sum;
                customer.OrdersCount = count;
            }
            return customersDto;
        }
    }
}
