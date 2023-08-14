using MediatR;
using TestTask_Infopulse.BLL.Commands.ProductCommands;
using TestTask_Infopulse.BLL.CustomExceptions;
using TestTask_Infopulse.BLL.Services.LoggerService;
using TestTask_Infopulse.DataAccess.Entities;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces.Custom;

namespace TestTask_Infopulse.BLL.Handlers.ProductHandlers
{
    internal class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductsRepository _productsRepository;
        private readonly IGenericRepository<ProductCategory> _productCategoriesRepository;
        private readonly ILoggerManager _loggerManager;

        public UpdateProductHandler(
            IUnitOfWork unitOfWork,
            ILoggerManager loggerManager)
        {
            _loggerManager = loggerManager;
            _unitOfWork = unitOfWork;
            _productsRepository = _unitOfWork.GetRepository<Product, IProductsRepository>();
            _productCategoriesRepository = _unitOfWork.GetRepository<ProductCategory>();
        }
        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productsRepository.GetFirstOrDefaultAsync(p => p.Id == request.EditProductDTO.Id);
            if (product == null)
            {
                _loggerManager.LogError($"Error updating the product. Product with id {request.EditProductDTO.Id} was not found.");
                throw new DataProcessingException(System.Net.HttpStatusCode.NotFound, 
                    $"Product with id {request.EditProductDTO.Id} was not found.");
            }

            product.AvailableQuantity = request.EditProductDTO.AvailableQuantity;
            product.ProductName = request.EditProductDTO.ProductName;
            product.Description = request.EditProductDTO.Description;
            product.Price = request.EditProductDTO.Price;
            product.ProductSize = request.EditProductDTO.ProductSize;

            var productCategory = await _productCategoriesRepository.GetFirstOrDefaultAsync(pc =>
                pc.Id == request.EditProductDTO.ProductCategory.Id);
            if (productCategory == null)
            {
                var newProductCategory = new ProductCategory
                {
                    Name = request.EditProductDTO.ProductCategory.Name
                };
                await _productCategoriesRepository.CreateAsync(newProductCategory);
                product.Category = newProductCategory;
            }
            else
            {
                product.Category = productCategory;
            }

            _productsRepository.Update(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _loggerManager.LogInfo($"Product with id {product.Id} was updated.");
            return Unit.Value;
        }
    }
}
