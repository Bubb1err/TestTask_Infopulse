using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestTask_Infopulse.BLL.CustomExceptions;
using TestTask_Infopulse.BLL.Queries.ProductQueries;
using TestTask_Infopulse.BLL.Services.LoggerService;
using TestTask_Infopulse.BLL.ViewModels;
using TestTask_Infopulse.DataAccess.Entities;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces.Custom;

namespace TestTask_Infopulse.BLL.Handlers.ProductHandlers
{
    internal class GetProductHandler : IRequestHandler<GetProductQuery, ProductDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;
        private readonly IProductsRepository _productsRepository;
        public GetProductHandler(
            IUnitOfWork unitOfWork,
            ILoggerManager loggerManager, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _loggerManager = loggerManager;
            _mapper = mapper;
            _productsRepository = _unitOfWork.GetRepository<Product, IProductsRepository>();
        }
        public async Task<ProductDTO> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productsRepository.GetFirstOrDefaultAsync(p => p.Id == request.Id, include: source => 
                source.Include(p => p.Category));
            if (product == null)
            {
                _loggerManager.LogError($"Error getting the product. Product with id {request.Id} was not found.");
                throw new DataProcessingException(System.Net.HttpStatusCode.NotFound,
                    $"Product with id {request.Id} was not found.");
            }
            var productDto = _mapper.Map<ProductDTO>(product);
            return productDto;
        }
    }
}
