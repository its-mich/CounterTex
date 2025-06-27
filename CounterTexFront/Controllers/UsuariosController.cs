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
        public ActionResult Editar()
        {
            var usuario = Session["Usuario"] as LoginResponse;
            if (usuario == null)
                return RedirectToAction("Login", "Auth");

            var model = new EditarNombreViewModel
            {
                Id = usuario.Id,
                Nombre = usuario.Nombres
            };

            return View(model); // Buscará Views/Usuarios/Editar.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(EditarNombreViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string apiUrl = ConfigurationManager.AppSettings["Api"];
            string urlGet = $"{apiUrl}/usuarios/GetUsuariosId/{model.Id}";
            string urlPut = $"{apiUrl}/usuarios/{model.Id}";

            using (var client = new HttpClient())
            {
                try
                {
                    var responseGet = await client.GetAsync(urlGet);
                    if (!responseGet.IsSuccessStatusCode)
                    {
                        TempData["Error"] = "No se pudo obtener el usuario.";
                        return View(model);
                    }

                    var usuarioJson = await responseGet.Content.ReadAsStringAsync();
                    var usuarioActual = JsonConvert.DeserializeObject<UsuarioApiDto>(usuarioJson);

                    if (usuarioActual == null)
                    {
                        TempData["Error"] = "Usuario no encontrado.";
                        return View(model);
                    }

                    // ✅ Modificamos solo los campos necesarios
                    usuarioActual.Correo = model.Correo;
                    usuarioActual.Telefono = model.Telefono;
                    usuarioActual.Edad = model.Edad;

                    // ✅ Enviamos el objeto completo requerido por el backend
                    var content = new StringContent(JsonConvert.SerializeObject(usuarioActual), Encoding.UTF8, "application/json");
                    var responsePut = await client.PutAsync(urlPut, content);

                    if (responsePut.IsSuccessStatusCode)
                    {
                        TempData["Success"] = "Usuario actualizado correctamente.";
                        return RedirectToAction("Editar", new { id = model.Id }); // se mantiene como Editar
                    }

                    TempData["Error"] = $"Error al actualizar el usuario ({(int)responsePut.StatusCode}).";
                    return View(model);
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Excepción: " + ex.Message;
                    return View(model);
                }
            }
        }

    }
}