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
    public class ConsultarInformacionController : BaseController
    {
        private readonly HttpClient _httpClient;
        private readonly string apiUrl;

        public ConsultarInformacionController()
        {
            apiUrl = ConfigurationManager.AppSettings["Api"];
            if (string.IsNullOrEmpty(apiUrl))
                throw new ArgumentNullException(nameof(apiUrl), "La clave 'Api' no está definida en Web.config o es nula.");

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(apiUrl)
            };
        }

        // ✅ Mostrar todas las asignaciones
        public async Task<ActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Prendas/GetPrendas");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "No se pudo obtener la lista de prendas.";
                return View(new List<Models.ConsultarInformacionViewModel>());
            }

            var json = await response.Content.ReadAsStringAsync();
            var prendas = JsonConvert.DeserializeObject<List<Models.ConsultarInformacionViewModel>>(json);

            return View(prendas);
        }

        // ✅ Detalle por ID
        public async Task<ActionResult> Detalles(int id)
        {
            var response = await _httpClient.GetAsync($"api/Prendas/GetPrendaById/{id}");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "No se pudo obtener la prenda.";
                return RedirectToAction("Index");
            }

            var json = await response.Content.ReadAsStringAsync();
            var prenda = JsonConvert.DeserializeObject<Models.ConsultarInformacionViewModel>(json);

            return View(prenda);
        }
    }
}
