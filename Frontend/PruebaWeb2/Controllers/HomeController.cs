using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PruebaWeb2.Models;
using RestSharp;
using System.Diagnostics;

namespace PruebaWeb2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {


            return View();
        }

       
    }
}
