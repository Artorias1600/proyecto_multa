using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PruebaWeb2.Models;
using RestSharp;
using System.Diagnostics;

namespace PruebaWeb2.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var client = new RestClient();
            var request = new RestRequest("http://localhost:9098/api/Usuarios", Method.Get);
            RestResponse response = await client.ExecuteAsync(request);
            List<Usuario> usuarios = JsonConvert.DeserializeObject<List<Usuario>>(response.Content);

            Console.WriteLine(response.Content);

            return View(usuarios);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarUsuario(Usuario us)
        {
            var client = new RestClient();
            var request = new RestRequest("http://localhost:9098/api/Usuarios", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(us);
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUsuario(UsuarioEditar us)
        {
            Usuario us1 = new Usuario();
            us1.UsuarioID = us.UsuarioIDEditar;
            us1.Nombre = us.NombreEditar;
            us1.Contrasena = us.ContrasenaEditar;

            var client = new RestClient();
            var request = new RestRequest("http://localhost:9098/api/Usuarios/" + us.UsuarioIDEditar, Method.Put);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(us1);
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> EliminarUsuario(int Id)
        {

            var client = new RestClient();
            var request = new RestRequest("http://localhost:9098/api/Usuarios/" + Id, Method.Delete);
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
