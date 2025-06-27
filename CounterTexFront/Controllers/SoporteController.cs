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
    /// Controlador responsable de gestionar la sección de soporte y contacto.
    /// </summary>
    public class SoporteController : BaseController
    {
        /// <summary>
        /// URL base de la API definida en Web.config.
        /// </summary>
        private readonly string apiUrl = ConfigurationManager.AppSettings["Api"];

        /// <summary>
        /// Acción que carga y muestra la lista de contactos registrados desde la API.
        /// </summary>
        /// <returns>Vista con la lista de contactos.</returns>
        public async Task<ActionResult> Index()
        {
            List<ContactoViewModel> contactos = new List<ContactoViewModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                var response = await client.GetAsync("api/Contacto/GetContactos");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    contactos = JsonConvert.DeserializeObject<List<ContactoViewModel>>(json);
                }
            }

            return View(contactos);
        }

        /// <summary>
        /// Acción que registra un nuevo contacto (asunto y mensaje) en la API.
        /// </summary>
        /// <param name="Asunto">Título o motivo del mensaje.</param>
        /// <param name="Mensaje">Contenido del mensaje del usuario.</param>
        /// <returns>Redirige a la vista Index tras el registro.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegistrarContacto(string Asunto, string Mensaje)
        {
            var contacto = new ContactoViewModel
            {
                Asunto = Asunto,
                Mensaje = Mensaje
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                var content = new StringContent(JsonConvert.SerializeObject(contacto), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/Contacto/PostContacto", content);

                if (!response.IsSuccessStatusCode)
                    ModelState.AddModelError("", "Error al registrar el contacto.");
            }

            return RedirectToAction("Index");
        }
    }
}
