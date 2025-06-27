using CounterTexFront.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CounterTexFront.Controllers
{
    /// <summary>
    /// Controlador encargado de manejar la visualización de metas y horarios del usuario.
    /// </summary>
    public class MetaController : BaseController
    {
        /// <summary>
        /// URL base de la API, leída desde Web.config.
        /// </summary>
        private readonly string apiUrl = ConfigurationManager.AppSettings["Api"];

        /// <summary>
        /// Acción principal que carga las metas y horarios asignados al usuario actual.
        /// </summary>
        /// <returns>Vista con el ViewModel que contiene metas y horarios del usuario.</returns>
        public async Task<ActionResult> Index()
        {
            // ✅ Verifica si hay un usuario autenticado
            var usuario = Session["Usuario"] as LoginResponse;
            if (usuario == null)
                return RedirectToAction("Login", "Auth");

            int usuarioId = usuario.Id;

            // 🧾 Inicializa el ViewModel
            var viewModel = new MetaYHorarioViewModel
            {
                Metas = new List<MetaViewModel>(),
                Horarios = new List<HorarioViewModel>()
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);

                // 📥 Obtener metas del usuario
                HttpResponseMessage response = await client.GetAsync($"api/Metas/GetMeta/{usuarioId}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    viewModel.Metas = JsonConvert.DeserializeObject<List<MetaViewModel>>(json) ?? new List<MetaViewModel>();
                }

                // 📥 Obtener horarios del usuario
                HttpResponseMessage responsHorarios = await client.GetAsync($"api/Horarios/GetHorario/{usuarioId}");
                if (responsHorarios.IsSuccessStatusCode)
                {
                    var json = await responsHorarios.Content.ReadAsStringAsync();
                    viewModel.Horarios = JsonConvert.DeserializeObject<List<HorarioViewModel>>(json) ?? new List<HorarioViewModel>();
                }

                return View(viewModel);
            }
        }
    }
}
