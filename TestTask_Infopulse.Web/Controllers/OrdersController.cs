using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestTask_Infopulse.BLL.Commands.OrderCommands;
using TestTask_Infopulse.BLL.Queries.OrderQueries;
using TestTask_Infopulse.BLL.ViewModels;
using TestTask_Infopulse.DataAccess.Entities.Enums;

namespace TestTask_Infopulse.Web.Controllers
{
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("orders")]
        [ResponseCache(CacheProfileName = CacheProfiles.FiveSeconds)]
        public async Task<IActionResult> GetAllOrders()
        {
            var getAllOrdersQuery = new GetAllOrdersQuery();
            var result = await _mediator.Send(getAllOrdersQuery);
            return Ok(result);
        }
        [HttpGet("order")]
        public async Task<IActionResult> GetOrder(int? id)
        {
            if (id == null) { return BadRequest("Order`s id could not be null."); }
            var getOrderQuery = new GetOrderQuery((int)id);
            var result = await _mediator.Send(getOrderQuery);
            return Ok(result);

        }
        [HttpPost("order")]
        public async Task<IActionResult> CreateOrder([FromBody]CreateOrderDTO createOrderDTO)
        {
            if(!ModelState.IsValid) { return BadRequest(ModelState); }
            var createOrderCommand = new CreateOrderCommand(createOrderDTO);
            var result = await _mediator.Send(createOrderCommand);
            return Ok(result);
        }
        [HttpPut("order")]
        public async Task<IActionResult> UpdateOrder([FromBody]EditOrderDTO editOrderDTO)
        {
            if (!ModelState.IsValid)  return BadRequest(editOrderDTO);
            var editOrderCommand = new EditOrderCommand(editOrderDTO);
            await _mediator.Send(editOrderCommand);
            return Ok();
        }
        [HttpDelete("order")]
        public async Task<IActionResult> DeleteOrder(int? id)
        {
            if (id == null) return BadRequest("Id could not be null.");
            var deleteOrderCommand = new DeleteOrderCommand((int)id);
            await _mediator.Send(deleteOrderCommand);

            return Ok();
        }
        [HttpGet("statuses")]
        [ResponseCache(CacheProfileName = CacheProfiles.FiveSeconds)]
        public IActionResult GetStatuses()
        {
            Dictionary<int, string> statuses = Enum.GetValues(typeof(Status)).Cast<Status>()
                .ToDictionary(t => (int)t, t => t.ToString());
            return Ok(statuses);
        }
    }
}
