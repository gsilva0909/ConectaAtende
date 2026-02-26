using Microsoft.AspNetCore.Mvc;

namespace API.ConectaAtende.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var produtos = new[] { "Produto 1", "Produto 2", "Produto 3" };
            return Ok(produtos);
        }
    }
}