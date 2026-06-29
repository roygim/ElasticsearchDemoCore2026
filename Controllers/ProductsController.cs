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

        [HttpPost("add")]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            var result = await _service.AddProductAsync(dto);
            if (result.success)
                return Ok(result);

            return result.error == ErrorType.AlreadyExists
                ? Conflict(result)
                : BadRequest(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string q)
        {
            var result = await _service.SearchAsync(q);
            if (result.success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var result = await _service.GetByCategoryIdAsync(categoryId);
            if (result.success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductDto dto)
        {
            var result = await _service.UpdateProductAsync(id, dto);
            if (result.success)
                return Ok(result);

            return result.error == ErrorType.NotFound
                ? NotFound(result)
                : BadRequest(result);
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
