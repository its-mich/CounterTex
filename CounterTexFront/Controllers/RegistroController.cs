using CounterTexFront.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CounterTexFront.Controllers
{
    public class RegistroController : Controller
    {
        string apiUrl = ConfigurationManager.AppSettings["Api"].ToString();

        public ActionResult Registro()
        {
            return View();
        }

        public async Task<ActionResult> Index()
        {
            List<RegistroViewModel> registros = new List<RegistroViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                HttpResponseMessage response = await client.GetAsync("api/Registro/GetRegistro");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    registros = JsonConvert.DeserializeObject<List<RegistroViewModel>>(jsonResponse);
                }
            }
            return View(registros);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registro(RegistroViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Por favor verifica los campos del formulario.");
                return View(model);
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Clear();

                string json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("api/Auth/Registro", content);

                if (Res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login", "Auth");
                }
                else
                {
                    var errorResponse = await Res.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", "Error al registrar el usuario: " + errorResponse);
                    return View(model);
                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegistroViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Si el modelo no es válido, mostramos un mensaje de error
                ModelState.AddModelError("", "Por favor verifica los campos del formulario.");
                return View(model);
            }

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Clear();

                    // Serializamos el modelo a JSON
                    string json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    // Enviar la solicitud POST
                    HttpResponseMessage response = await client.PostAsync("api/Auth/Registro", content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Si la respuesta es exitosa, mostramos un mensaje de éxito
                        ViewBag.Message = "¡Registro exitoso! Ahora puedes iniciar sesión.";
                    }
                    else
                    {
                        // Si la respuesta no es exitosa, mostramos el mensaje de error
                        var errorResponse = await response.Content.ReadAsStringAsync();
                        ViewBag.Message = "Error al registrar el usuario: " + errorResponse;
                    }
                }
            }
            catch (HttpRequestException httpEx)
            {
                // Manejo de excepciones de conexión
                ViewBag.Message = "Error de conexión con el servidor: " + httpEx.Message;
            }
            catch (Exception ex)
            {
                // Captura de errores inesperados
                ViewBag.Message = "Error inesperado: " + ex.Message;
            }

            // Devolvemos la vista con el mensaje (sin redirección)
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RegistroViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                string json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PutAsync("api/Registro/PutRegistro", content);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                await client.DeleteAsync($"api/Registro/DeleteRegistro/{id}");
            }
            return RedirectToAction("Index");
        }
    }
}
