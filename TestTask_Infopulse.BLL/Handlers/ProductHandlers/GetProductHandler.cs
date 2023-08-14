using MediatR;
using TestTask_Infopulse.BLL.CustomExceptions;
using TestTask_Infopulse.BLL.Queries.ProductQueries;
using TestTask_Infopulse.BLL.Services.LoggerService;
using TestTask_Infopulse.DataAccess.Entities;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces.Custom;

namespace TestTask_Infopulse.BLL.Handlers.ProductHandlers
{
    internal class GetProductHandler : IRequestHandler<GetProductQuery, Product>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _loggerManager;
        private readonly IProductsRepository _productsRepository;
        public GetProductHandler(
            IUnitOfWork unitOfWork,
            ILoggerManager loggerManager)
        {
            _unitOfWork = unitOfWork;
            _loggerManager = loggerManager;
            _productsRepository = _unitOfWork.GetRepository<Product, IProductsRepository>();
        }
        public async Task<Product> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productsRepository.GetFirstOrDefaultAsync(p => p.Id == request.Id);
            if (product == null)
            {
                _loggerManager.LogError($"Error getting the product. Product with id {request.Id} was not found.");
                throw new DataProcessingException(System.Net.HttpStatusCode.NotFound,
                    $"Product with id {request.Id} was not found.");
            }
            return product;
        }
    }
}
