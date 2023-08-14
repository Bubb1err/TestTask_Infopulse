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
    internal class GetCustomersSelectListHandler : IRequestHandler<GetCustomersSelectListQuery, List<SelectListCustomerDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICustomersRepository _customersRepository;

        public GetCustomersSelectListHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _customersRepository = _unitOfWork.GetRepository<Customer, ICustomersRepository>();
        }
        public async Task<List<SelectListCustomerDTO>> Handle(GetCustomersSelectListQuery request, CancellationToken cancellationToken)
        {
            var customers = await _customersRepository.GetAll().ToListAsync();
            var customersDto = _mapper.Map<List<SelectListCustomerDTO>>(customers);
            return customersDto;
        }
    }
}
