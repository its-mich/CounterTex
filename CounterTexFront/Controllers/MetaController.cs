using CounterTexFront.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CounterTexFront.Controllers
{
    public class MetaController : BaseController
    {
        private readonly string apiUrl = ConfigurationManager.AppSettings["Api"];

        // GET: Meta/Index
        public async Task<ActionResult> Index()
        {
            var usuario = Session["Usuario"] as LoginResponse;
            if (usuario == null)
                return RedirectToAction("Login", "Auth");

            int usuarioId = usuario.Id;

            var viewModel = new MetaYHorarioViewModel
            {
                Metas = new List<MetaViewModel>(),
                Horarios = new List<HorarioViewModel>()
            };

            using (var client = new HttpClient())
            {
                // Establece la URL base y el encabezado de autorización con el token
                client.BaseAddress = new Uri(apiUrl);

                // Solicita todos los productos a la API
                HttpResponseMessage response = await client.GetAsync($"api/Metas/GetMeta/{usuarioId}");

                // Lee y deserializa el contenido de la respuesta
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    viewModel.Metas = JsonConvert.DeserializeObject<List<MetaViewModel>>(json) ?? new List<MetaViewModel>();
                }

                // Cargar horarios
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
