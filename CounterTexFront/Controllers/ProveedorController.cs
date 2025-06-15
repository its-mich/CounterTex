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

            return View(proveedores); // Cambio aquí para la vista "Index"
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
        // GET: Lista de entregas
        public async Task<ActionResult> IndexEntregas()
        {
            List<PrendasEntregadasViewModel> entregas = new List<PrendasEntregadasViewModel>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    HttpResponseMessage response = await client.GetAsync("api/Prendas");

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        entregas = JsonConvert.DeserializeObject<List<PrendasEntregadasViewModel>>(json);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al cargar entregas: " + ex.Message);
            }

            return View(entregas);
        }
        public ActionResult CreateEntrega()
        {
            return View();
        }

        // POST: Crear entrega
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateEntrega(PrendasEntregadasViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    string json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("api/Prendas", content);
                    if (!response.IsSuccessStatusCode)
                        ModelState.AddModelError("", "No se pudo crear la entrega.");
                }

                return RedirectToAction("IndexEntregas");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error: " + ex.Message);
                return View(model);
            }
        }

        public ActionResult EditEntrega()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> EditEntrega(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    HttpResponseMessage response = await client.GetAsync($"api/Prendas/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var prenda = JsonConvert.DeserializeObject<PrendasEntregadasViewModel>(json);
                        return View(prenda); // ← esto llena los campos del formulario
                    }
                }

                TempData["Error"] = "No se encontró la prenda.";
                return RedirectToAction("IndexEntregas");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar los datos: " + ex.Message;
                return RedirectToAction("IndexEntregas");
            }
        }


        //POST: Editar entrega
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditEntrega(PrendasEntregadasViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    string json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync($"api/Prendas/{model.Id}", content);
                    if (!response.IsSuccessStatusCode)
                        ModelState.AddModelError("", "No se pudo editar la entrega.");
                }

                return RedirectToAction("IndexEntregas");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error: " + ex.Message);
                return View(model);
            }
        }

        public ActionResult DeleteEntrega()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteEntrega(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    HttpResponseMessage response = await client.DeleteAsync($"api/Prendas/{id}");

                    if (!response.IsSuccessStatusCode)
                        return new HttpStatusCodeResult(500, "No se pudo eliminar la prenda");
                }

                return new HttpStatusCodeResult(200); // OK
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, "Error interno: " + ex.Message);
            }
        }

    }
}
