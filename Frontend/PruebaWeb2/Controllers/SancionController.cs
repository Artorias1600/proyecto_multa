using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PruebaWeb2.Models;
using RestSharp;
using System.Diagnostics;

namespace PruebaWeb2.Controllers
{
    public class SancionController : Controller
    {
        private readonly ILogger<SancionController> _logger;

        public SancionController(ILogger<SancionController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var client = new RestClient();
            var request = new RestRequest("http://localhost:9098/api/Sanciones", Method.Get);
            RestResponse response = await client.ExecuteAsync(request);
            List<Sancion> sanciones = JsonConvert.DeserializeObject<List<Sancion>>(response.Content);

            Console.WriteLine(response.Content);

            return View(sanciones);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarSancion(Sancion san)
        {
            var client = new RestClient();
            var request = new RestRequest("http://localhost:9098/api/Sanciones", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(san);
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSancion(SancionEditar san)
        {
            Sancion san1 = new Sancion();
            san1.SancionID = san.SancionIDEditar;
            san1.Descripcion = san.DescripcionEditar;
            san1.Costo = san.CostoEditar;

            var client = new RestClient();
            var request = new RestRequest("http://localhost:9098/api/Sanciones/" + san.SancionIDEditar, Method.Put);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(san1);
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> EliminarSancion(int Id)
        {

            var client = new RestClient();
            var request = new RestRequest("http://localhost:9098/api/Sanciones/" + Id, Method.Delete);
            request.AddHeader("Content-Type", "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
