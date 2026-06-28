using Microsoft.AspNetCore.Mvc;

namespace DemoCore2026.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _service;

        public ProductsController(IProductsService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var result = await _service.AddProductAsync(product);
            if (result.success)
                return Ok(result);

            return result.error == ErrorType.AlreadyExists
                ? Conflict(result)
                : BadRequest(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string q)
        {
            var result = await _service.SearchAsync(q);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result.success)
                return Ok(result);

            return result.error == ErrorType.NotFound
                ? NotFound(result)
                : BadRequest(result);
        }
    }
}
