using Microsoft.AspNetCore.Mvc;

namespace DemoCore2026.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ItemsController : ControllerBase
    {
        public ItemsController()
        {
        }
            
        [HttpGet("all")]
        public async Task<ActionResult> GetAll()
        {
            var response = "ok";
            return Ok(response);
        }
    }
}
