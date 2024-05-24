using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PruebaWeb2.Models;
using RestSharp;
using System.Diagnostics;

namespace PruebaWeb2.Controllers
{
    public class VehiculoController : Controller
    {
        private readonly ILogger<VehiculoController> _logger;

        public VehiculoController(ILogger<VehiculoController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var client = new RestClient();
            var request = new RestRequest("http://localhost:9098/api/Vehiculos", Method.Get);
            RestResponse response = await client.ExecuteAsync(request);
            List<Vehiculo> vehiculos = JsonConvert.DeserializeObject<List<Vehiculo>>(response.Content);
            Console.WriteLine(response.Content);
            return View(vehiculos);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarVehiculo(Vehiculo vh)
        {
            var client = new RestClient();
            var request = new RestRequest("http://localhost:9098/api/Vehiculos", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(vh);
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateVehiculo(VehiculoEditar vh)
        {
        Vehiculo vh1 = new Vehiculo();
            vh1.VehiculoID = vh.VehiculoIDEditar;
            vh1.Tipo = vh.TipoEditar;
            vh1.Marca = vh.MarcaEditar;
            vh1.Modelo = vh.ModeloEditar;
            vh1.Placa = vh.PlacaEditar;

            var client = new RestClient();
            var request = new RestRequest("http://localhost:9098/api/Vehiculos/" + vh.VehiculoIDEditar, Method.Put);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(vh1);
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> EliminarVehiculo(int Id)
        {

            var client = new RestClient();
            var request = new RestRequest("http://localhost:9098/api/Vehiculos/" + Id, Method.Delete);
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
