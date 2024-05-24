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

            //var options = new RestClientOptions()
            //{
            //    MaxTimeout = -1,

            //};
            var client = new RestClient();
            //var request = new RestRequest("http://192.168.1.151:5239/api/AgentesTransito", Method.Get);
            var request = new RestRequest("http://localhost:9098/api/AgentesTransito", Method.Get);
            RestResponse response = await client.ExecuteAsync(request);
            List<AgenteTransito> agentes = JsonConvert.DeserializeObject<List<AgenteTransito>>(response.Content);

            Console.WriteLine(response.Content);

            return View(agentes);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarAgente(AgenteTransito ag)
        {

            //var options = new RestClientOptions("")
            //{
            //    MaxTimeout = -1,
            //};
            var client = new RestClient();
            //var request = new RestRequest("http://192.168.1.151:5239/api/AgentesTransito", Method.Post);
            var request = new RestRequest("http://localhost:9098/api/AgentesTransito", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(ag);
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAgente(AgenteTransitoEditar ag)
        {

            //var options = new RestClientOptions("")
            //{
            //    MaxTimeout = -1,
            //};

            AgenteTransito ag1 = new AgenteTransito();
            ag1.AgenteID = ag.AgenteIdEditar;
            ag1.Nombre = ag.NombreEditar;
            ag1.Placa = ag.PlacaEditar;

            var client = new RestClient();
            //var request = new RestRequest("http://192.168.1.151:5239/api/AgentesTransito/"+ag.AgenteIdEditar, Method.Put);
            var request = new RestRequest("http://localhost:9098/api/AgentesTransito/" + ag.AgenteIdEditar, Method.Put);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(ag1);
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
            return RedirectToAction("Index");
        }



        [HttpPost]
        public async Task<IActionResult> EliminarAgente(int Id)
        {

            var client = new RestClient();
            //var request = new RestRequest("http://192.168.1.151:5239/api/AgentesTransito/" + Id, Method.Delete);
            var request = new RestRequest("http://localhost:9098/api/AgentesTransito/" + Id, Method.Delete);
            request.AddHeader("Content-Type", "application/json");
            //var body = JsonConvert.SerializeObject(ag1);
            //request.AddStringBody(body, DataFormat.Json);
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
