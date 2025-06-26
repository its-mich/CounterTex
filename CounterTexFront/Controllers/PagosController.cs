using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Collections.Generic;
using CounterTexFront.Models;
using Newtonsoft.Json;
using System;

namespace CounterTexFront.Controllers
{
    public class PagosController : BaseController
    {
        private readonly string apiUrl = ConfigurationManager.AppSettings["Api"].ToString();

        public async Task<ActionResult> Index()
        {
            var pagos = new List<PagoViewModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(apiUrl.TrimEnd('/'));

                try
                {
                    var response = await client.GetAsync("/api/Pagos");
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();

                        pagos = JsonConvert.DeserializeObject<List<PagoViewModel>>(json);

                        // 👉 Aquí revisas si la propiedad Usuario vino nula
                        if (pagos.Count > 0)
                        {
                            foreach (var p in pagos)
                            {
                                if (p.Usuario == null)
                                {
                                    System.Diagnostics.Debug.WriteLine($"⚠️ Usuario NULL para pago con ID {p.Id}");
                                }
                                else
                                {
                                    System.Diagnostics.Debug.WriteLine($"✅ Usuario OK: {p.Usuario.Nombre}");
                                }
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", $"Error al obtener los pagos. Código: {response.StatusCode}");
                    }
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError("", "Error al conectar con la API: " + ex.Message);
                }
            }

            return View(pagos);
        }

        [HttpPost]
        public async Task<ActionResult> Generar(DateTime fechaInicio, DateTime fechaFin)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl.TrimEnd('/'));

                var fechas = new
                {
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin
                };

                var content = new StringContent(
                    JsonConvert.SerializeObject(fechas),
                    System.Text.Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PostAsync("/api/Pagos/generar", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "✅ Nómina generada correctamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = $"❌ Error al generar nómina: {response.ReasonPhrase}";
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> MisPagos()
        {
            var usuario = Session["Usuario"] as LoginResponse;

            if (usuario == null)
            {
                TempData["ErrorMessage"] = "Sesión expirada. Por favor, inicia sesión nuevamente.";
                return RedirectToAction("Login", "Auth");
            }

            // Log de depuración del ID
            System.Diagnostics.Debug.WriteLine($"🟡 ID del usuario autenticado: {usuario.Id}");

            var pagos = new List<PagoViewModel>();

            using (var client = new HttpClient())
            {
                // ✅ Elimina /api del baseAddress si ya está en la ruta del endpoint
                var baseUri = apiUrl.Replace("/api", "").TrimEnd('/');
                client.BaseAddress = new Uri(baseUri);

                // ✅ Autenticación con Bearer token si aplica
                if (Session["BearerToken"] != null)
                {
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["BearerToken"].ToString());
                }

                try
                {
                    var endpoint = $"/api/Pagos/usuario/{usuario.Id}";
                    var response = await client.GetAsync(endpoint);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        pagos = JsonConvert.DeserializeObject<List<PagoViewModel>>(json);
                    }
                    else
                    {
                        TempData["ErrorMessage"] = $"Error al obtener sus pagos. Código: {response.StatusCode}";
                    }
                }
                catch (HttpRequestException ex)
                {
                    TempData["ErrorMessage"] = "Error de conexión con el servidor: " + ex.Message;
                }
            }

            return View("MisPagos", pagos);
        }


    }
}
