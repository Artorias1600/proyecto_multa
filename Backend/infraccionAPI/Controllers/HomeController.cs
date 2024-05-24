using Microsoft.AspNetCore.Mvc;

namespace infraccionAPI.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("BIENVENIDO A InfraccionVehicular API");
        }
    }
}
