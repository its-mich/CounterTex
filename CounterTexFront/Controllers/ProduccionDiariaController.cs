using Newtonsoft.Json;
using CounterTexFront.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CounterTexFront.Models.DTOs;

namespace CounterTexFront.Controllers
{
    public class ProduccionDiariaController : BaseController
    {
        private readonly string apiUrl = ConfigurationManager.AppSettings["Api"];

        public async Task<ActionResult> Index()
        {
            ProduccionDiariaViewModel model = new ProduccionDiariaViewModel();

            try
            {
                model.Fecha = DateTime.Now;
                model.Usuarios = await ObtenerUsuarios();
                model.Prendas = await ObtenerPrendas();
                model.Operaciones = await ObtenerOperaciones();
                model.ProduccionDetalles = new List<ProduccionDiariaDetalleViewModel>
                {
                    new ProduccionDiariaDetalleViewModel()
                };
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al cargar el formulario: " + ex.Message);
            }

            return View(model);
        }

        [HttpPost]
        // [ValidateAntiForgeryToken] // Puedes dejarlo comentado si aún no usas el token
        public async Task<ActionResult> Index(ProduccionDiariaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Usuarios = await ObtenerUsuarios();
                model.Prendas = await ObtenerPrendas();
                model.Operaciones = await ObtenerOperaciones();
                TempData["ErrorMessage"] = "❌ Por favor completa todos los campos requeridos correctamente.";
                return View(model);
            }

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("api/Produccion", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        TempData["ErrorMessage"] = "❌ Error al guardar la producción diaria.";
                        model.Usuarios = await ObtenerUsuarios();
                        model.Prendas = await ObtenerPrendas();
                        model.Operaciones = await ObtenerOperaciones();
                        return View(model);
                    }
                }

                TempData["SuccessMessage"] = "✅ Producción registrada correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "⚠️ Error inesperado: " + ex.Message;
                model.Usuarios = await ObtenerUsuarios();
                model.Prendas = await ObtenerPrendas();
                model.Operaciones = await ObtenerOperaciones();
                return View(model);
            }
        }


        // --- Métodos auxiliares para obtener listas desde la API ---

        private async Task<IEnumerable<SelectListItem>> ObtenerUsuarios()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    HttpResponseMessage response = await client.GetAsync("api/Usuarios/GetUsuarios");

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(json);

                        return usuarios.Select(u => new SelectListItem
                        {
                            Text = u.Nombre,
                            Value = u.Id.ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al obtener usuarios: " + ex.Message);
            }

            return new List<SelectListItem>();
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerPrendas()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    HttpResponseMessage response = await client.GetAsync("api/Prendas");

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var prendas = JsonConvert.DeserializeObject<List<Prenda>>(json);

                        return prendas.Select(p => new SelectListItem
                        {
                            Text = p.Nombre,
                            Value = p.Id.ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al obtener prendas: " + ex.Message);
            }

            return new List<SelectListItem>();
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerOperaciones()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    HttpResponseMessage response = await client.GetAsync("api/Operacion");

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var operaciones = JsonConvert.DeserializeObject<List<Operacion>>(json);

                        return operaciones.Select(o => new SelectListItem
                        {
                            Text = o.Nombre,
                            Value = o.Id.ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al obtener operaciones: " + ex.Message);
            }

            return new List<SelectListItem>();
        }



        public async Task<ActionResult> ObtenerRegistrosPorUsuario(int usuarioId)
        {
            string endpoint = $"{apiUrl}/produccion/empleado/{usuarioId}";

            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(endpoint);

                    if (!response.IsSuccessStatusCode)
                        return Json(new List<object>(), JsonRequestBehavior.AllowGet);

                    var json = await response.Content.ReadAsStringAsync();

                    // Asegúrate de que ProduccionApiDto esté definido correctamente en tu proyecto
                    var producciones = JsonConvert.DeserializeObject<List<ProduccionApiDto>>(json);

                    var resultado = producciones.Select(p => new
                    {
                        Id = p.Id,
                        Fecha = p.Fecha.ToString("yyyy-MM-dd"),
                        PrendaNombre = p.Prenda?.Nombre ?? "N/A",
                        TotalCantidad = p.ProduccionDetalles?.Sum(d => d.Cantidad) ?? 0,
                        TotalValor = p.TotalValor ?? 0 // <-- esto estaba faltando
                    });


                    return Json(resultado, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { error = "Error al conectar con la API: " + ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }


        [HttpGet]
        public async Task<ActionResult> Editar(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                var response = await client.GetAsync($"api/Produccion/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    TempData["ErrorMessage"] = "⚠️ No se pudo cargar la producción.";
                    return RedirectToAction("Index");
                }

                var json = await response.Content.ReadAsStringAsync();
                var produccion = JsonConvert.DeserializeObject<ProduccionDiariaViewModel>(json);

                produccion.Usuarios = await ObtenerUsuarios();
                produccion.Prendas = await ObtenerPrendas();
                produccion.Operaciones = await ObtenerOperaciones();

                return View("Editar", produccion); // Vista Razor llamada Editar.cshtml
            }
        }

        [HttpPost]
        public async Task<ActionResult> Editar(ProduccionDiariaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Usuarios = await ObtenerUsuarios();
                model.Prendas = await ObtenerPrendas();
                model.Operaciones = await ObtenerOperaciones();
                TempData["ErrorMessage"] = "❌ Completa los datos correctamente.";
                return View(model);
            }

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PutAsync($"api/Produccion/{model.Id}", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        TempData["ErrorMessage"] = "❌ No se pudo actualizar la producción.";
                        return View(model);
                    }

                    TempData["SuccessMessage"] = "✅ Producción actualizada correctamente.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "⚠️ Error al actualizar: " + ex.Message;
                return View(model);
            }
        }




    }
}
