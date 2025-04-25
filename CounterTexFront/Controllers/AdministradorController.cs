using Newtonsoft.Json;
using CounterTexFront.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CounterTexFront.Controllers
{
    public class AdministradorController : Controller
    {
        string apiUrl = ConfigurationManager.AppSettings["Api"].ToString();

        // Acción para obtener la lista de administradores
        public async Task<ActionResult> Index()
        {
            List<PerfilAdministrador> administradores = new List<PerfilAdministrador>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    HttpResponseMessage response = await client.GetAsync("api/Administrador/GetAdministrador");

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        administradores = JsonConvert.DeserializeObject<List<PerfilAdministrador>>(jsonResponse);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error al obtener los datos de administradores.");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al conectarse con el servidor: " + ex.Message);
            }

            return View("Administrador", administradores); // Cambio aquí para la vista "Administrador"
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PerfilAdministrador model)
        {
            if (!ModelState.IsValid)
                return View("Administrador", model); // Cambio aquí para la vista "Administrador"

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    string json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("api/Administrador/PostAdministrador", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("", "No se pudo crear el administrador. Inténtalo nuevamente.");
                        return View("Administrador", model); // Cambio aquí para la vista "Administrador"
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al crear el administrador: " + ex.Message);
                return View("Administrador", model); // Cambio aquí para la vista "Administrador"
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PerfilAdministrador model)
        {
            if (!ModelState.IsValid)
                return View("Administrador", model); // Cambio aquí para la vista "Administrador"

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    string json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync("api/Administrador/PutAdministrador", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("", "No se pudo editar el administrador. Inténtalo nuevamente.");
                        return View("Administrador", model); // Cambio aquí para la vista "Administrador"
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al editar el administrador: " + ex.Message);
                return View("Administrador", model); // Cambio aquí para la vista "Administrador"
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    HttpResponseMessage response = await client.DeleteAsync($"api/Administrador/DeleteAdministrador/{id}");

                    if (!response.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("", "No se pudo eliminar el administrador. Inténtalo nuevamente.");
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar el administrador: " + ex.Message);
                return RedirectToAction("Index");
            }
        }
    }
}
