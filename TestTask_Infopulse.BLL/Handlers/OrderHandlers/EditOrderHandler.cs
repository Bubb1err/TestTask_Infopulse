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
    public class EditOrderHandler : IRequestHandler<EditOrderCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _loggerManager;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IOrderedProductsRepository _orderedProductsRepository;
        private readonly ICustomersRepository _customersRepository;
        private readonly IProductsRepository _productsRepository;
        public EditOrderHandler(
            IUnitOfWork unitOfWork,
            ILoggerManager loggerManager)
        {
            _unitOfWork = unitOfWork;
            _loggerManager = loggerManager;
            _ordersRepository = _unitOfWork.GetRepository<Order, IOrdersRepository>();
            _orderedProductsRepository = _unitOfWork.GetRepository<OrderedProduct, IOrderedProductsRepository>();  
            _customersRepository = _unitOfWork.GetRepository<Customer, ICustomersRepository>();
            _productsRepository = _unitOfWork.GetRepository<Product, IProductsRepository>();
        }
        public async Task<Unit> Handle(EditOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _ordersRepository.GetFirstOrDefaultAsync(o => o.Id == request.EditOrderDTO.Id, include: source => 
                source.Include(o => o.OrderedProducts));

            if (order == null)
            {
                _loggerManager.LogError($"Error updating order. Order with number {request.EditOrderDTO.Id} was not found.");
                throw new DataProcessingException(System.Net.HttpStatusCode.NotFound,
                $"Order with number {request.EditOrderDTO.Id} was not found.");
            }

            order.Status = (Status)request.EditOrderDTO.Status;
            order.Comment = request.EditOrderDTO.Comment;

            var customer = await _customersRepository.GetFirstOrDefaultAsync(c => c.Id == request.EditOrderDTO.CustomerId);
            if (customer == null)
            {
                _loggerManager.LogError($"Error updating order. Customer with id {request.EditOrderDTO.CustomerId} does not exist.");
                throw new DataProcessingException(System.Net.HttpStatusCode.BadRequest,
                    $"Customer with id {request.EditOrderDTO.CustomerId} does not exist.");
            }
            order.Customer = customer;
            order.CustomerId = customer.Id;

            List<OrderedProduct> orderedProducts = new List<OrderedProduct>();
            foreach (var orderedProduct in request.EditOrderDTO.OrderedProducts)
            {
                if (orderedProduct.Id != null)
                {
                    var existingOrdredProduct = await _orderedProductsRepository.GetFirstOrDefaultAsync(p => 
                        p.Id == orderedProduct.Id);
                    if (existingOrdredProduct != null)
                    {
                        continue;
                    }
                }
                var product = await _productsRepository.GetFirstOrDefaultAsync(p => p.Id == orderedProduct.ProductId);
                if (product == null)
                {
                    _loggerManager.LogError($"Error updating order. Product with id {orderedProduct.ProductId} does not exist.");
                    throw new DataProcessingException(System.Net.HttpStatusCode.BadRequest,
                        $"Product with id {orderedProduct.ProductId} does not exist.");
                }
                var orderedProductsInDb = await _orderedProductsRepository.GetAll(p => p.ProductId == orderedProduct.ProductId).ToListAsync();
                int orderedQuantity = orderedProductsInDb.Sum(p => p.Quantity);

                if (orderedQuantity + orderedProduct.Quantity > product.AvailableQuantity)
                {
                    _loggerManager.LogError($"Error updating order. Quantity of { product.ProductName} is bigger than available quantity.");
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
                order.OrderedProducts.Add(newOrderedProduct);
            }
            _orderedProductsRepository.AddRange(orderedProducts);

            order.UpdateTotalCost();
            _ordersRepository.Update(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _loggerManager.LogInfo($"Order with id {order.Id} was updated");

            return Unit.Value;
        }
    }
}
