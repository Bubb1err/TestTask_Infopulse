using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestTask_Infopulse.BLL.Commands.CustomerCommands;
using TestTask_Infopulse.BLL.Queries.CustomerQueries;
using TestTask_Infopulse.BLL.ViewModels;

namespace TestTask_Infopulse.Web.Controllers
{
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("customers")]
        [ResponseCache(CacheProfileName = CacheProfiles.FiveSeconds)]
        public async Task<IActionResult> GetCustomers()
        {
            var getCustomersQuery = new GetCustomersQuery();
            var result = await _mediator.Send(getCustomersQuery);
            return Ok(result);
        }
        [HttpGet("select-customers")]
        [ResponseCache(CacheProfileName = CacheProfiles.FiveSeconds)]
        public async Task<IActionResult> GetCustomersSelectList()
        {
            var getSelectListCustomersQuery = new GetCustomersSelectListQuery();
            var result = await _mediator.Send(getSelectListCustomersQuery);
            return Ok(result);
        }
        [HttpPost("customer")]
        public async Task<IActionResult> CreateCustomer([FromBody]CreateCustomerDTO createCustomerDTO)
        {
            if (!ModelState.IsValid) { return BadRequest(createCustomerDTO); }
            var createCustomerCommand = new CreateCustomerComand(createCustomerDTO);
            await _mediator.Send(createCustomerCommand);
            return Ok();
        }
    }
}
