using MediatR;
using Microsoft.EntityFrameworkCore;
using TestTask_Infopulse.BLL.Commands.OrderCommands;
using TestTask_Infopulse.BLL.CustomExceptions;
using TestTask_Infopulse.BLL.Services.LoggerService;
using TestTask_Infopulse.DataAccess.Entities;
using TestTask_Infopulse.DataAccess.Entities.Enums;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces.Custom;

namespace TestTask_Infopulse.BLL.Handlers.OrderHandlers
{
    internal class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _loggerManager;
        private readonly IOrdersRepository _ordersRepository;
        private readonly ICustomersRepository _customersRepository;
        private readonly IOrderedProductsRepository _orderedProductsRepository;
        private readonly IProductsRepository _productsRepository;
        public CreateOrderHandler(
            IUnitOfWork unitOfWork,
            ILoggerManager loggerManager)
        {
            _unitOfWork = unitOfWork;
            _loggerManager = loggerManager;
            _ordersRepository = _unitOfWork.GetRepository<Order, IOrdersRepository>();
            _customersRepository = _unitOfWork.GetRepository<Customer, ICustomersRepository>();
            _orderedProductsRepository = _unitOfWork.GetRepository<OrderedProduct, IOrderedProductsRepository>();
            _productsRepository = _unitOfWork.GetRepository<Product, IProductsRepository>();
        }
        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderInDatabase = await _ordersRepository.GetFirstOrDefaultAsync(o => o.OrderNumber == request.CreateOrderDTO.OrderNumber);
            if (orderInDatabase != null)
            {
                _loggerManager.LogError($"Error creating a new order. Order with number {request.CreateOrderDTO.OrderNumber} is already exists.");
                throw new DataProcessingException(System.Net.HttpStatusCode.BadRequest,
                    $"Order with number {request.CreateOrderDTO.OrderNumber} is already exists.");
            }

            var order = new Order
            {
                OrderNumber = request.CreateOrderDTO.OrderNumber,
                Status = (Status)request.CreateOrderDTO.Status,
                Comment = request.CreateOrderDTO.Comment ?? string.Empty,
            };

            var customer = await _customersRepository.GetFirstOrDefaultAsync(c => c.Id == request.CreateOrderDTO.CustomerId);
            if(customer == null)
            {
                _loggerManager.LogError($"Error creating a new order. Customer with id {request.CreateOrderDTO.CustomerId} does not exist.");
                throw new DataProcessingException(System.Net.HttpStatusCode.BadRequest,
                    $"Customer with id {request.CreateOrderDTO.CustomerId} does not exist.");
            }
            order.Customer = customer;
            order.CustomerId = customer.Id;

            List<OrderedProduct> orderedProducts = new List<OrderedProduct>();
            foreach (var orderedProduct in request.CreateOrderDTO.OrderedProducts)
            {
                var product = await _productsRepository.GetFirstOrDefaultAsync(p => p.Id == orderedProduct.ProductId);
                if (product == null)
                {
                    _loggerManager.LogError($"Error creating a new order. Product with id {orderedProduct.ProductId} does not exist.");

                    throw new DataProcessingException(System.Net.HttpStatusCode.BadRequest, 
                        $"Product with id {orderedProduct.ProductId} does not exist.");
                }
                var orderedProductsInDb = await _orderedProductsRepository.GetAll(p => p.ProductId == orderedProduct.ProductId).ToListAsync();
                int orderedQuantity = orderedProductsInDb.Sum(p => p.Quantity);

                if (orderedQuantity + orderedProduct.Quantity > product.AvailableQuantity)
                {
                    _loggerManager.LogError($"Error creating a new order. Quantity of {product.ProductName} is bigger than available quantity.");
                    throw new DataProcessingException(System.Net.HttpStatusCode.BadRequest,
                        $"Quantity of {product.ProductName} is bigger than available quantity.");
                }
                var newOrderedProduct = new OrderedProduct
                {
                    Product = product,
                    Quantity = orderedProduct.Quantity,
                    ProductId = product.Id
                };
                orderedProducts.Add(newOrderedProduct);
            }
            _orderedProductsRepository.AddRange(orderedProducts);

            order.OrderedProducts = orderedProducts;
            order.UpdateTotalCost();

            await _ordersRepository.CreateAsync(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _loggerManager.LogInfo($"New order created.");

            return Unit.Value;
        }
    }
}
