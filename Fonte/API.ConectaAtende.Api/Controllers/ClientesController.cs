using Microsoft.AspNetCore.Mvc;

namespace API.ConectaAtende.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // dummy data
            var clientes = new[] { "Cliente A", "Cliente B", "Cliente C" };
            return Ok(clientes);
        }
    }
}