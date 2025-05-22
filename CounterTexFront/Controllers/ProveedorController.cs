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
    public class ProveedorController : BaseController
    {
        string apiUrl = ConfigurationManager.AppSettings["Api"].ToString();

        public async Task<ActionResult> Index()
        {
            List<ProveedorViewModel> proveedores = new List<ProveedorViewModel>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    HttpResponseMessage response = await client.GetAsync("api/Proveedor/GetProveedor");

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        proveedores = JsonConvert.DeserializeObject<List<ProveedorViewModel>>(jsonResponse);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error al obtener los datos de proveedores.");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al conectarse con el servidor: " + ex.Message);
            }

            return View("Proveedor", proveedores); // Cambio aquí para la vista "Proveedor"
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProveedorViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Proveedor", model); // Cambio aquí para la vista "Proveedor"

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    string json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("api/Proveedor/PostProveedor", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("", "No se pudo crear el proveedor. Inténtalo nuevamente.");
                        return View("Proveedor", model); // Cambio aquí para la vista "Proveedor"
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al crear el proveedor: " + ex.Message);
                return View("Proveedor", model); // Cambio aquí para la vista "Proveedor"
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProveedorViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Proveedor", model); // Cambio aquí para la vista "Proveedor"

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    string json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync("api/Proveedor/PutProveedor", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("", "No se pudo editar el proveedor. Inténtalo nuevamente.");
                        return View("Proveedor", model); // Cambio aquí para la vista "Proveedor"
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al editar el proveedor: " + ex.Message);
                return View("Proveedor", model); // Cambio aquí para la vista "Proveedor"
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
                    HttpResponseMessage response = await client.DeleteAsync($"api/Proveedor/DeleteProveedor/{id}");

                    if (!response.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("", "No se pudo eliminar el proveedor. Inténtalo nuevamente.");
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar el proveedor: " + ex.Message);
                return RedirectToAction("Index");
            }
        }
    }
}
