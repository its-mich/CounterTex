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
    public class SoporteController : BaseController
    {
        private readonly string apiUrl = ConfigurationManager.AppSettings["Api"];

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
