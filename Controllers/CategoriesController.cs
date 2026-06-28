using Microsoft.AspNetCore.Mvc;

namespace DemoCore2026.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _service;

        public CategoriesController(ICategoriesService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Create(Category category)
        {
            var result = await _service.AddCategoryAsync(category);
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
