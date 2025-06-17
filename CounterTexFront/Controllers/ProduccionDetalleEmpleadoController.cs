using CounterTexFront.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CounterTexFront.Controllers
{
    public class ProduccionDetalleEmpleadoController : BaseController
    {
        private readonly HttpClient _httpClient;
        private readonly string apiUrl;

        public ProduccionDetalleEmpleadoController()
        {
            apiUrl = ConfigurationManager.AppSettings["Api"];
            _httpClient = new HttpClient { BaseAddress = new Uri(apiUrl) };
        }
        public async Task<ActionResult> ProduccionDetalleEmpleado(string color = "", string prenda = "")
        {
            var response = await _httpClient.GetAsync("api/ProduccionDetalle");
            var json = await response.Content.ReadAsStringAsync();
            var lista = JsonConvert.DeserializeObject<List<ProduccionDetalleEmpleadoViewModel>>(json);

            // Usamos LINQ para filtrar
            var resultado = lista.AsQueryable();

            if (!string.IsNullOrWhiteSpace(color))
            {
                string colorLower = color.ToLower();
                resultado = resultado.Where(p => p.ColorPrenda != null && p.ColorPrenda.ToLower().Contains(colorLower));
            }

            if (!string.IsNullOrWhiteSpace(prenda))
            {
                string prendaLower = prenda.ToLower();
                resultado = resultado.Where(p => p.NombrePrenda != null && p.NombrePrenda.ToLower().Contains(prendaLower));
            }

            return View(resultado.ToList());
        }

        public async Task<ActionResult> Detalle(int id)
        {
            var response = await _httpClient.GetAsync($"api/ProduccionDetalle/{id}");
            var json = await response.Content.ReadAsStringAsync();
            var detalle = JsonConvert.DeserializeObject<ProduccionDetalleEmpleadoViewModel>(json);

            return View(detalle);
        }
    }
}