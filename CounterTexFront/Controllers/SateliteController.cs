using CounterTexFront.Models;
using Newtonsoft.Json;
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
    /// <summary>
    /// Controlador para la gestión de Satélites.
    /// Permite consultar, crear, editar y eliminar registros de satélites mediante la API.
    /// </summary>
    public class SateliteController : BaseController
    {
        /// <summary>
        /// URL base de la API, tomada desde Web.config.
        /// </summary>
        string apiUrl = ConfigurationManager.AppSettings["Api"].ToString();

        /// <summary>
        /// Obtiene la lista de satélites desde la API y la envía a la vista.
        /// </summary>
        /// <returns>Vista con la lista de satélites.</returns>
        public async Task<ActionResult> Index()
        {
            List<SateliteViewModel> satelites = new List<SateliteViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                HttpResponseMessage response = await client.GetAsync("api/Satelite/GetSatelite");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    satelites = JsonConvert.DeserializeObject<List<SateliteViewModel>>(jsonResponse);
                }
            }
            return View(satelites);
        }

        /// <summary>
        /// Crea un nuevo satélite enviando la información a la API.
        /// </summary>
        /// <param name="model">Modelo del satélite a crear.</param>
        /// <returns>Redirige a Index si tiene éxito; si no, devuelve la vista con errores.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SateliteViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                string json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PostAsync("api/Satelite/PostSatelite", content);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Edita un satélite existente enviando los cambios a la API.
        /// </summary>
        /// <param name="model">Modelo actualizado del satélite.</param>
        /// <returns>Redirige a Index si tiene éxito; si no, devuelve la vista con errores.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SateliteViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                string json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PutAsync("api/Satelite/PutSatelite", content);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Elimina un satélite por ID llamando a la API.
        /// </summary>
        /// <param name="id">ID del satélite a eliminar.</param>
        /// <returns>Redirige a Index luego de la eliminación.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                await client.DeleteAsync($"api/Satelite/DeleteSatelite/{id}");
            }
            return RedirectToAction("Index");
        }
    }
}
