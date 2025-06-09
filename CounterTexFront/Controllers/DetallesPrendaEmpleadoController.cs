using CounterTexFront.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CounterTexFront.Controllers
{
    public class DetallesPrendaEmpleadoController : Controller
    {
        private readonly HttpClient _httpClient;

        public DetallesPrendaEmpleadoController()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:7030/") // ⚠️ Cambia según la URL de tu API
            };

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public ActionResult DetallesPrendaEmpleado()
        {
            return View(new DetallesPrendaEmpleadoViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> GuardarProduccion(DetallesPrendaEmpleadoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Hay errores en el formulario.";
                return View("DetallesPrendaEmpleado", model);
            }

            try
            {
                // Aquí debes cargar los IDs reales según tu lógica
                int usuarioId = 1; // TODO: reemplazar con usuario real
                int prendaId = 1;  // TODO: reemplazar con prenda real
                int operacionId = 1; // TODO: reemplazar con operación real

                var produccion = new DetallesPrendaEmpleadoViewModel
                {
                    UsuarioId = usuarioId,
                    PrendaId = prendaId,
                    OperacionId = operacionId,
                    Fecha = model.Fecha,
                    Cantidad = model.Cantidad,
                    Color = model.Color,
                    TipoPrenda = model.TipoPrenda,
                    Modelo = model.Modelo,
                    Folio = model.Folio,
                    Operacion = model.Operacion,
                    Observaciones = model.Observaciones,
                    Talla = model.Talla,
                    Efecto = model.Efecto,
                    Acabado = model.Acabado
                };

                var json = JsonConvert.SerializeObject(produccion);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/Produccion", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Mensaje"] = "Producción enviada correctamente.";
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    TempData["Error"] = $"Error al guardar producción: {error}";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Excepción: {ex.Message}";
            }

            return RedirectToAction("DetallesPrendaEmpleado");
        }
    }
}
