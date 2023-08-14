using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestTask_Infopulse.BLL.Queries.ProductQueries;
using TestTask_Infopulse.BLL.ViewModels;
using TestTask_Infopulse.DataAccess.Entities;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces.Custom;

namespace TestTask_Infopulse.BLL.Handlers.ProductHandlers
{
    internal class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<ProductDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;
        public GetAllProductsHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productsRepository = _unitOfWork.GetRepository<Product, IProductsRepository>();
        }
        public async Task<List<ProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productsRepository.GetAll(include: 
                source => source.Include(p => p.Category)).ToListAsync();
            var productsDto = _mapper.Map<List<ProductDTO>>(products);
            return productsDto;
        }
    }
}
