using Microsoft.AspNetCore.Mvc;

namespace API.ConectaAtende.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var pedidos = new[] { "Pedido X", "Pedido Y" };
            return Ok(pedidos);
        }
    }
}