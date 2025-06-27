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
        public ActionResult EditarPerfil()
        {
            var usuario = Session["Usuario"] as LoginResponse;
            if (usuario == null)
                return RedirectToAction("Login", "Auth");

            var model = new EditarPerfilViewModel
            {
                Id = usuario.Id,
                Nombres = usuario.Nombres,
                Correo = usuario.Correo,
                Telefono = usuario.Telefono,
                Edad = usuario.Edad,
                Documento = usuario.Documento
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditarPerfil(EditarPerfilViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var usuario = Session["Usuario"] as LoginResponse;
            if (usuario == null)
                return RedirectToAction("Login", "Auth");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Api"]);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var token = Session["BearerToken"] as string;
                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                var dto = new
                {
                    id = model.Id,
                    nombre = model.Nombres,
                    correo = model.Correo,
                    documento = model.Documento,
                    contraseña = (string)null,   
                    rolId = usuario.RolId,
                    edad = model.Edad,
                    telefono = model.Telefono,
                    rolNombre = usuario.RolNombre
                };

                var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"api/Usuarios/{model.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    // Actualizar datos de sesión
                    usuario.Nombres = model.Nombres;
                    usuario.Correo = model.Correo;
                    usuario.Telefono = model.Telefono;
                    usuario.Edad = model.Edad;
                    usuario.Documento = model.Documento;
                    Session["Usuario"] = usuario;

                    TempData["Success"] = "Perfil actualizado correctamente.";
                    return RedirectToAction("EditarPerfil");
                }

                // Obtener y mostrar el detalle del error
                var errorContent = await response.Content.ReadAsStringAsync();
                TempData["Error"] = $"Error ({(int)response.StatusCode}): {await response.Content.ReadAsStringAsync()}";
                return View(model);
            }
        }
    }
}