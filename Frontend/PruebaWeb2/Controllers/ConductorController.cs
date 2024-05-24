using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PruebaWeb2.Models;
using RestSharp;
using System.Diagnostics;

namespace PruebaWeb2.Controllers
{
    public class ConductorController : Controller
    {
        private readonly ILogger<ConductorController> _logger;

        public ConductorController(ILogger<ConductorController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var client = new RestClient();
            var request = new RestRequest("http://localhost:9098/api/Conductores", Method.Get);
            RestResponse response = await client.ExecuteAsync(request);
            List<Conductor> agentes = JsonConvert.DeserializeObject<List<Conductor>>(response.Content);

            Console.WriteLine(response.Content);

            return View(agentes);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarConductor(Conductor con)
        {
            var client = new RestClient();
            var request = new RestRequest("http://localhost:9098/api/Conductores", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(con);
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> UpdateConductor(ConductorEditar con)
        {
            Conductor con1 = new Conductor();
            con1.ConductorID = con.ConductorIDEditar;
            con1.Nombre = con.NombreEditar;
            con1.Licencia = con.LicenciaEditar;

            var client = new RestClient();
            var request = new RestRequest("http://localhost:9098/api/Conductores/" + con.ConductorIDEditar, Method.Put);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(con1);
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EliminarConductor(int Id)
        {
            var client = new RestClient();
            var request = new RestRequest("http://localhost:9098/api/Conductores/" + Id, Method.Delete);
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
