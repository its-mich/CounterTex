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
    public class MetaController : Controller
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

            using (HttpClient client = new HttpClient())
            {
                // Cargar metas
                var responseMetas = await client.GetAsync($"{apiUrl}/api/Meta/PorUsuario/{usuarioId}");
                if (responseMetas.IsSuccessStatusCode)
                {
                    var json = await responseMetas.Content.ReadAsStringAsync();
                    viewModel.Metas = JsonConvert.DeserializeObject<List<MetaViewModel>>(json);
                }

                // Cargar horarios
                var responseHorarios = await client.GetAsync($"{apiUrl}/api/Horario/PorUsuario/{usuarioId}");
                if (responseHorarios.IsSuccessStatusCode)
                {
                    var json = await responseHorarios.Content.ReadAsStringAsync();
                    viewModel.Horarios = JsonConvert.DeserializeObject<List<HorarioViewModel>>(json);
                }
            }

            return View(viewModel);
        }


        // GET: Meta/Crear
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Meta/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear(MetaViewModel model)
        {
            var usuario = Session["Usuario"] as LoginResponse;
            if (usuario == null)
                return RedirectToAction("Login", "Auth");

            // Asignar automáticamente el UsuarioId
            model.UsuarioId = usuario.Id;

            if (!ModelState.IsValid)
                return View(model);

            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{apiUrl}/api/Meta", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError("", "Error al guardar la meta.");
            return View(model);
        }

        // GET: Meta/Eliminar/{id}
        public async Task<ActionResult> Eliminar(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                await client.DeleteAsync($"{apiUrl}/api/Meta/{id}");
            }

            return RedirectToAction("Index");
        }
    }
}
