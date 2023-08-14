using AutoMapper;
using MediatR;
using TestTask_Infopulse.BLL.Commands.ProductCommands;
using TestTask_Infopulse.BLL.CustomExceptions;
using TestTask_Infopulse.BLL.Services.LoggerService;
using TestTask_Infopulse.DataAccess.Entities;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces.Custom;

namespace TestTask_Infopulse.BLL.Handlers.ProductHandlers
{
    internal class CreateProductHandler : IRequestHandler<CreateProductCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductsRepository _productsRepository;
        private readonly IGenericRepository<ProductCategory> _productCategoriesRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _loggerManager;

        public CreateProductHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILoggerManager loggerManager)
        {
            _mapper = mapper;
            _loggerManager = loggerManager;
            _unitOfWork = unitOfWork;
            _productsRepository = _unitOfWork.GetRepository<Product,  IProductsRepository>();
            _productCategoriesRepository = _unitOfWork.GetRepository<ProductCategory>();

        }
        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productInDb = await _productsRepository.GetFirstOrDefaultAsync(p => 
                p.ProductNumber == request.CreateProductDTO.ProductNumber);

            if(productInDb != null)
            {
                _loggerManager.LogError($"Error creating a new product. Product with number {request.CreateProductDTO.ProductNumber} is already exist.");
                throw new DataProcessingException(System.Net.HttpStatusCode.BadRequest,
                    $"Product with number {request.CreateProductDTO.ProductNumber} is already exist.");
            }
            var product = _mapper.Map<Product>(request.CreateProductDTO);
            var productCategory = await _productCategoriesRepository.GetFirstOrDefaultAsync(pc =>
                pc.Id == request.CreateProductDTO.ProductCategory.Id);
            if (productCategory == null)
            {
                var newProductCategory = new ProductCategory
                {
                    Name = request.CreateProductDTO.ProductCategory.Name
                };
                await _productCategoriesRepository.CreateAsync(newProductCategory);
                product.Category = newProductCategory;
            }
            else
            {
                product.Category = productCategory;
            }

            await _productsRepository.CreateAsync(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
