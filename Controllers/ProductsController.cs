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
            await _service.AddProductAsync(product);
            return Ok();
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
            var product = await _service.GetByIdAsync(id);
            return Ok(product);
        }
    }
}
