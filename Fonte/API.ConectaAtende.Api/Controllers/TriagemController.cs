using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.ConectaAtende.Api.Controllers
{
    [Route("[controller]")]
    public class TriagemController : Controller
    {
        public TriagemController()
        {
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            var produtos = new[] { "Produto 1", "Produto 2", "Produto 3" };
            return Ok(produtos);
        }
    }
}