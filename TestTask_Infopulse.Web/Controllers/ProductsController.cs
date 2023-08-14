using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestTask_Infopulse.BLL.Commands.ProductCommands;
using TestTask_Infopulse.BLL.Queries.ProductQueries;
using TestTask_Infopulse.BLL.ViewModels;

namespace TestTask_Infopulse.Web.Controllers
{
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("products")]
        [ResponseCache(CacheProfileName = CacheProfiles.FiveSeconds)]
        public async Task<IActionResult> GetAllProducts()
        {
            var getAllProductsQuery = new GetAllProductsQuery();
            var result = await _mediator.Send(getAllProductsQuery);
            return Ok(result);
        }
        [HttpGet("product")]
        public async Task<IActionResult> GetProduct(int? id)
        {
            if(id == null) { return BadRequest("Id could not be null."); }
            var getProductQuery = new GetProductQuery((int)id);
            var result = await _mediator.Send(getProductQuery);
            return Ok(result);
        }
        [HttpGet("product-categories")]
        [ResponseCache(CacheProfileName = CacheProfiles.FiveSeconds)]
        public async Task<IActionResult> GetProductCategories()
        {
            var getProductCategoriesQuery = new GetProductCategoriesQuery();
            var result = await _mediator.Send(getProductCategoriesQuery);
            return Ok(result);
        }

        [HttpPost("product")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO createProductDTO)
        {
            if (!ModelState.IsValid) return BadRequest(createProductDTO);
            var createProductCommand = new CreateProductCommand(createProductDTO);
            await _mediator.Send(createProductCommand);
            return Ok();
        }
        [HttpPut("product")]
        public async Task<IActionResult> UpdateProduct([FromBody]EditProductDTO productDto)
        {
            if (!ModelState.IsValid) { return BadRequest(productDto); }
            var updateProductCommand = new UpdateProductCommand(productDto);
            await _mediator.Send(updateProductCommand);
            return Ok();
        }
    }
}
