using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Collections.Generic;
using CounterTexFront.Models;
using Newtonsoft.Json;
using System;
using Rotativa;

namespace CounterTexFront.Controllers
{
    /// <summary>
    /// Controlador encargado de gestionar pagos y generar reportes en PDF.
    /// </summary>
    public class PagosController : BaseController
    {
        private readonly string apiUrl = ConfigurationManager.AppSettings["Api"].ToString();

        /// <summary>
        /// Muestra la lista completa de pagos.
        /// </summary>
        /// <returns>Vista con la lista de pagos.</returns>
        public async Task<ActionResult> Index()
        {
            var pagos = new List<PagoViewModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl.TrimEnd('/'));

                try
                {
                    var response = await client.GetAsync("/api/Pagos");
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        pagos = JsonConvert.DeserializeObject<List<PagoViewModel>>(json);
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

        /// <summary>
        /// Genera la nómina de pagos entre dos fechas específicas.
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio del periodo.</param>
        /// <param name="fechaFin">Fecha de fin del periodo.</param>
        /// <returns>Redirecciona a la vista Index con mensaje.</returns>
        [HttpPost]
        public async Task<ActionResult> Generar(DateTime fechaInicio, DateTime fechaFin)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl.TrimEnd('/'));

                var fechas = new { FechaInicio = fechaInicio, FechaFin = fechaFin };

                var content = new StringContent(JsonConvert.SerializeObject(fechas), System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/Pagos/generar", content);

                if (response.IsSuccessStatusCode)
                    TempData["SuccessMessage"] = "✅ Nómina generada correctamente.";
                else
                    TempData["ErrorMessage"] = $"❌ Error al generar nómina: {response.ReasonPhrase}";
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Muestra los pagos del usuario autenticado.
        /// </summary>
        /// <returns>Vista con los pagos personales del usuario.</returns>
        public async Task<ActionResult> MisPagos()
        {
            var usuario = Session["Usuario"] as LoginResponse;
            if (usuario == null)
            {
                TempData["ErrorMessage"] = "Sesión expirada. Por favor, inicia sesión nuevamente.";
                return RedirectToAction("Login", "Auth");
            }

            var pagos = new List<PagoViewModel>();

            using (var client = new HttpClient())
            {
                var baseUri = apiUrl.Replace("/api", "").TrimEnd('/');
                client.BaseAddress = new Uri(baseUri);

                if (Session["BearerToken"] != null)
                {
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["BearerToken"].ToString());
                }

                try
                {
                    var response = await client.GetAsync($"/api/Pagos/usuario/{usuario.Id}");

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

        /// <summary>
        /// Genera un archivo PDF con los pagos del usuario actual.
        /// </summary>
        /// <returns>Archivo PDF generado a partir de la vista 'MisPagosPdf'.</returns>
        public async Task<ActionResult> ExportarPdf()
        {
            var usuario = Session["Usuario"] as LoginResponse;

            if (usuario == null)
            {
                TempData["ErrorMessage"] = "Sesión expirada. Por favor, inicia sesión nuevamente.";
                return RedirectToAction("Login", "Auth");
            }

            var pagos = new List<PagoViewModel>();

            using (var client = new HttpClient())
            {
                var baseUri = apiUrl.Replace("/api", "").TrimEnd('/');
                client.BaseAddress = new Uri(baseUri);

                if (Session["BearerToken"] != null)
                {
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session["BearerToken"].ToString());
                }

                try
                {
                    var response = await client.GetAsync($"/api/Pagos/usuario/{usuario.Id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        pagos = JsonConvert.DeserializeObject<List<PagoViewModel>>(json);
                    }
                    else
                    {
                        TempData["ErrorMessage"] = $"Error al obtener sus pagos. Código: {response.StatusCode}";
                        return RedirectToAction("MisPagos");
                    }
                }
                catch (HttpRequestException ex)
                {
                    TempData["ErrorMessage"] = "Error de conexión con el servidor: " + ex.Message;
                    return RedirectToAction("MisPagos");
                }
            }

            return new ViewAsPdf("MisPagosPdf", pagos)
            {
                FileName = $"Nomina_{DateTime.Now:yyyyMMdd}.pdf",
                PageSize = Rotativa.Options.Size.A4,
                PageOrientation = Rotativa.Options.Orientation.Portrait
            };
        }
    }
}
