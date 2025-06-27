using Newtonsoft.Json;
using CounterTexFront.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Net.Http.Headers;

namespace CounterTexFront.Controllers
{
    /// <summary>
    /// Controlador para gestionar proveedores, prendas entregadas y pagos a proveedores.
    /// </summary>
    public class ProveedorController : BaseController
    {
        private readonly string apiUrl = ConfigurationManager.AppSettings["Api"];

        /// <summary>
        /// Muestra la lista de proveedores.
        /// </summary>
        public async Task<ActionResult> Index()
        {
            List<ProveedorViewModel> proveedores = new List<ProveedorViewModel>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    var response = await client.GetAsync("api/Proveedor/GetProveedor");

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        proveedores = JsonConvert.DeserializeObject<List<ProveedorViewModel>>(json);
                    }
                    else
                        ModelState.AddModelError("", "Error al obtener los datos de proveedores.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al conectarse con el servidor: " + ex.Message);
            }

            return View(proveedores);
        }

        /// <summary>
        /// Crea un nuevo proveedor.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProveedorViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Proveedor", model);

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("api/Proveedor/PostProveedor", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("", "No se pudo crear el proveedor.");
                        return View("Proveedor", model);
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al crear el proveedor: " + ex.Message);
                return View("Proveedor", model);
            }
        }

        /// <summary>
        /// Edita un proveedor existente.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProveedorViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Proveedor", model);

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PutAsync("api/Proveedor/PutProveedor", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("", "No se pudo editar el proveedor.");
                        return View("Proveedor", model);
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al editar el proveedor: " + ex.Message);
                return View("Proveedor", model);
            }
        }

        /// <summary>
        /// Elimina un proveedor por su ID.
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
                    var response = await client.DeleteAsync($"api/Proveedor/DeleteProveedor/{id}");

                    if (!response.IsSuccessStatusCode)
                        ModelState.AddModelError("", "No se pudo eliminar el proveedor.");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar el proveedor: " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        // ==============================
        // GESTIÓN DE ENTREGAS
        // ==============================

        /// <summary>
        /// Muestra el listado de prendas entregadas.
        /// </summary>
        public async Task<ActionResult> IndexEntregas()
        {
            List<PrendasEntregadasViewModel> entregas = new List<PrendasEntregadasViewModel>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    var response = await client.GetAsync("api/Prendas");

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

        /// <summary>
        /// Muestra el formulario para crear una nueva entrega.
        /// </summary>
        public ActionResult CreateEntrega() => View();

        /// <summary>
        /// Registra una nueva entrega de prendas.
        /// </summary>
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
                    var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("api/Prendas", content);
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

        /// <summary>
        /// Muestra el formulario de edición de entrega.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> EditEntrega(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    var response = await client.GetAsync($"api/Prendas/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var prenda = JsonConvert.DeserializeObject<PrendasEntregadasViewModel>(json);
                        return View(prenda);
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

        /// <summary>
        /// Guarda la edición de una entrega.
        /// </summary>
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
                    var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                    var response = await client.PutAsync($"api/Prendas/{model.Id}", content);
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

        /// <summary>
        /// Elimina una entrega.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteEntrega(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    var response = await client.DeleteAsync($"api/Prendas/{id}");

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

        // ==============================
        // REGISTRO DE PAGOS
        // ==============================

        /// <summary>
        /// Muestra formulario para registrar un pago a proveedor.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> RegistrarPago()
        {
            var model = new PagoProveedorViewModel();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    var response = await client.GetAsync("api/Prendas");

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var prendas = JsonConvert.DeserializeObject<List<PrendasEntregadasViewModel>>(json);
                        model.PrendasDisponibles = prendas.Select(p => new SelectListItem
                        {
                            Value = p.Id.ToString(),
                            Text = p.Nombre
                        }).ToList();
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "No se pudo cargar la lista de prendas.";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al cargar prendas: " + ex.Message;
            }

            return View(model);
        }

        /// <summary>
        /// Registra el pago de prendas a un proveedor.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> RegistrarPago(PagoProveedorViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var usuario = Session["Usuario"] as LoginResponse;
            if (usuario == null)
            {
                TempData["ErrorMessage"] = "Sesión expirada.";
                return RedirectToAction("Login", "Auth");
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var dto = new
                {
                    ProveedorId = usuario.Id,
                    PrendaId = model.PrendaId,
                    CantidadPrendas = model.CantidadPrendas,
                    PrecioUnitario = model.PrecioUnitario,
                    Observaciones = model.Observaciones
                };

                var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/Pagos/proveedor", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Pago registrado correctamente.";
                    return RedirectToAction("RegistrarPago");
                }

                ModelState.AddModelError("", "Ocurrió un error al registrar el pago.");
                return View(model);
            }
        }
    }
}
