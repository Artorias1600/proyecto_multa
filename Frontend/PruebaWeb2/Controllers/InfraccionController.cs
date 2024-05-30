using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PruebaWeb2.Models;
using RestSharp;

namespace PruebaWeb2.Controllers
{
    public class InfraccionController : Controller
    {
        private readonly ILogger<InfraccionController> _logger;

        public InfraccionController(ILogger<InfraccionController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var client = new RestClient();
            var request = new RestRequest("http://localhost:9098/api/Infracciones", Method.Get);
            RestResponse response = await client.ExecuteAsync(request);
            List<Infraccion> infracciones = JsonConvert.DeserializeObject<List<Infraccion>>(response.Content);

            return View(infracciones);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarInfraccion([FromForm] Infraccion infraccion)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", await GetInfracciones());
            }

            var client = new RestClient();
            var request = new RestRequest("http://localhost:9098/api/Infracciones", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(infraccion);
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, response.Content);
                return View("Index", await GetInfracciones());
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateInfraccion([FromForm] Infraccion infraccion)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", await GetInfracciones());
            }

            var client = new RestClient();
            var request = new RestRequest($"http://localhost:9098/api/Infracciones/{infraccion.InfraccionID}", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(infraccion);
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, response.Content);
                return View("Index", await GetInfracciones());
            }
        }

        [HttpPost]
        public async Task<IActionResult> EliminarInfraccion(int id)
        {
            var client = new RestClient();
            var request = new RestRequest($"http://localhost:9098/api/Infracciones/{id}", Method.Delete);
            request.AddHeader("Content-Type", "application/json");
            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, response.Content);
                return View("Index", await GetInfracciones());
            }
        }

        private async Task<List<Infraccion>> GetInfracciones()
        {
            var client = new RestClient();
            var request = new RestRequest("http://localhost:9098/api/Infracciones", Method.Get);
            RestResponse response = await client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<List<Infraccion>>(response.Content);
        }
    }
}

