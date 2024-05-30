using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PruebaWeb2.Models;
using RestSharp;

namespace PruebaWeb2.Controllers
{
    public class PagoController : Controller
    {
        private readonly ILogger<PagoController> _logger;

        public PagoController(ILogger<PagoController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var client = new RestClient();
            var request = new RestRequest("http://localhost:9098/api/Pagos", Method.Get);
            RestResponse response = await client.ExecuteAsync(request);
            List<Pago> pagos = JsonConvert.DeserializeObject<List<Pago>>(response.Content);

            return View(pagos);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarPago(Pago pago)
        {
            var client = new RestClient();
            var request = new RestRequest("http://localhost:9098/api/Pagos", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(pago);
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePago(Pago pago)
        {
            var client = new RestClient();
            var request = new RestRequest($"http://localhost:9098/api/Pagos/{pago.PagoID}", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(pago);
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EliminarPago(int id)
        {
            var client = new RestClient();
            var request = new RestRequest($"http://localhost:9098/api/Pagos/{id}", Method.Delete);
            request.AddHeader("Content-Type", "application/json");
            RestResponse response = await client.ExecuteAsync(request);

            return RedirectToAction("Index");
        }
    }
}
