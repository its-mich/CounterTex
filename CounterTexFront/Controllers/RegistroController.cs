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
    /// <summary>
    /// Controlador para la gestión de usuarios registrados.
    /// </summary>
    public class RegistroController : BaseController
    {
        private readonly string apiUrl = ConfigurationManager.AppSettings["Api"];

        /// <summary>
        /// Vista para registrar un nuevo usuario (GET).
        /// </summary>
        public ActionResult Registro()
        {
            return View();
        }

        /// <summary>
        /// Muestra el listado de usuarios registrados (GET).
        /// </summary>
        public async Task<ActionResult> Index()
        {
            var registros = new List<RegistroViewModel>();

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

        /// <summary>
        /// Crea un nuevo registro de usuario (POST).
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegistroViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Por favor verifica los campos del formulario.");
                return View(model);
            }

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Clear();

                    string json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("api/Auth/Registro", content);

                    if (response.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "✅ ¡Registro exitoso! Ahora puedes iniciar sesión.";
                    }
                    else
                    {
                        var errorResponse = await response.Content.ReadAsStringAsync();
                        ViewBag.Message = "❌ Error al registrar el usuario: " + errorResponse;
                    }
                }
            }
            catch (HttpRequestException httpEx)
            {
                ViewBag.Message = "⚠️ Error de conexión con el servidor: " + httpEx.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Message = "⚠️ Error inesperado: " + ex.Message;
            }

            return View(model);
        }

        /// <summary>
        /// Edita la información de un usuario registrado (POST).
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RegistroViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);

                    string json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    await client.PutAsync("api/Registro/PutRegistro", content);
                }

                TempData["SuccessMessage"] = "✅ Usuario actualizado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "❌ Error al actualizar usuario: " + ex.Message;
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Elimina un usuario registrado por su ID (POST).
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    await client.DeleteAsync($"api/Registro/DeleteRegistro/{id}");
                }

                TempData["SuccessMessage"] = "✅ Usuario eliminado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "❌ Error al eliminar usuario: " + ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
