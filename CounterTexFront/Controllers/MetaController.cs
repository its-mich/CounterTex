using CounterTexFront.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CounterTexFront.Controllers
{
    public class MetaController : Controller
    {
        private readonly string apiUrl = ConfigurationManager.AppSettings["Api"];

        // GET: Meta/Index
        public async Task<ActionResult> Index()
        {
            var usuario = Session["Usuario"] as LoginResponse;
            if (usuario == null)
                return RedirectToAction("Login", "Auth");

            int usuarioId = usuario.Id;

            var viewModel = new MetaYHorarioViewModel
            {
                Metas = new List<MetaViewModel>(),
                Horarios = new List<HorarioViewModel>()
            };

            using (var client = new HttpClient())
            {
                // Establece la URL base y el encabezado de autorización con el token
                client.BaseAddress = new Uri(apiUrl);

                // Solicita todos los productos a la API
                HttpResponseMessage response = await client.GetAsync($"api/Metas/GetMeta/{usuarioId}");

                // Lee y deserializa el contenido de la respuesta
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    viewModel.Metas = JsonConvert.DeserializeObject<List<MetaViewModel>>(json) ?? new List<MetaViewModel>();
                }

                // Cargar horarios
                HttpResponseMessage responsHorarios = await client.GetAsync($"api/Horarios/GetHorario/{usuarioId}");
                if (responsHorarios.IsSuccessStatusCode)
                {
                    var json = await responsHorarios.Content.ReadAsStringAsync();
                    viewModel.Horarios = JsonConvert.DeserializeObject<List<HorarioViewModel>>(json) ?? new List<HorarioViewModel>();
                }

                return View(viewModel);
            }
        }
        


        // GET: Meta/Crear
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Meta/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear(MetaViewModel model)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // Establece la URL base de la API
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Clear();

                    // Serializa el modelo a formato JSON
                    string json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    // Envía la solicitud POST al microservicio de usuarios
                    HttpResponseMessage response = await client.PostAsync("api/Metas/PostMetas", content);

                    // Verifica si la respuesta fue exitosa
                    if (response.IsSuccessStatusCode)
                    {
                        var res = await response.Content.ReadAsStringAsync();
                        // Aquí puedes manejar la respuesta si es necesario
                    }
                    else
                    {
                        // Captura errores y los guarda en TempData para mostrarlos en la vista
                        var errorContent = await response.Content.ReadAsStringAsync();
                        TempData["Error"] = $"Error de API: {response.StatusCode} - {errorContent}";
                        return RedirectToAction("Iniciar", "Home");
                    }
                }

                // Redirige al usuario a la vista principal si todo fue exitoso
                return RedirectToAction("Index", "Meta");
            }
            catch (Exception ex)
            {
                // Maneja cualquier excepción y muestra un mensaje de error
                TempData["Error"] = $"Hubo un error al procesar la solicitud: {ex.Message}";
                return RedirectToAction("Iniciar", "Home");
            }
        }

        // GET: Meta/Eliminar/{id}
        public async Task<ActionResult> Eliminar(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                await client.DeleteAsync($"{apiUrl}/api/Meta/{id}");
            }

            return RedirectToAction("Index");
        }
    }
}
