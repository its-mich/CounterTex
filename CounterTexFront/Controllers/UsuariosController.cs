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
using System.Web.Script.Serialization;

namespace CounterTexFront.Controllers
{
    public class UsuariosController : BaseController
    {
        public readonly string apiUrl = ConfigurationManager.AppSettings["Api"].ToString();

        public async Task<ActionResult> Index()
        {
            List<UsuarioViewModel> usuarios = new List<UsuarioViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                HttpResponseMessage response = await client.GetAsync("api/Usuarios/GetUsuarios");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    usuarios = JsonConvert.DeserializeObject<List<UsuarioViewModel>>(jsonResponse);
                }
            }
            return View(usuarios);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UsuarioViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                string json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PostAsync("api/Usuarios/PostUsuarios", content);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UsuarioViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                string json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PutAsync("api/Usuarios/PutUsuarios", content);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                await client.DeleteAsync($"api/Usuarios/DeleteUsuarios/{id}");
            }
            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult EditarNombre()
        {
            var usuario = Session["Usuario"] as LoginResponse;
            if (usuario == null)
                return RedirectToAction("Login", "Auth");

            var model = new EditarNombreViewModel
            {
                Id = usuario.Id,
                Nombres = usuario.Nombres
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditarNombre(EditarNombreViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var usuario = Session["Usuario"] as LoginResponse;
            if (usuario == null)
                return RedirectToAction("Login", "Auth");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Api"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var dto = new { Nombre = model.Nombres };

                var content = new StringContent(
                    new JavaScriptSerializer().Serialize(dto),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PutAsync($"api/Usuarios/ActualizarNombre/{usuario.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    usuario.Nombres = model.Nombres;
                    Session["Usuario"] = usuario;
                    TempData["Success"] = "Nombre actualizado correctamente.";
                    return RedirectToAction("EditarNombre");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    TempData["Error"] = "Error al actualizar el nombre. Detalle: " + error;
                    return View(model);
                }
            }
        }




    }
}