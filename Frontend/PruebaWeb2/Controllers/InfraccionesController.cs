using Microsoft.AspNetCore.Mvc;

namespace PruebaWeb2.Controllers
{
    public class InfraccionesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
