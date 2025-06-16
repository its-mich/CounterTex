using CounterTexFront.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CounterTexFront.Controllers
{
    [Authorize]
    public class AdministradorController : BaseController
    {
        private readonly string apiUrl = ConfigurationManager.AppSettings["Api"].ToString();

        private HttpClient GetClient()
        {
            var client = new HttpClient { BaseAddress = new Uri(apiUrl) };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var token = System.Web.HttpContext.Current.Session["BearerToken"]?.ToString();
            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client;
        }

        // LISTAR USUARIOS
        public async Task<ActionResult> Index()
        {
            List<UsuarioViewModel> usuarios = new List<UsuarioViewModel>();
            using (var client = GetClient())
            {
                HttpResponseMessage resp = await client.GetAsync("api/Usuarios/GetUsuarios");
                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();
                    usuarios = JsonConvert.DeserializeObject<List<UsuarioViewModel>>(json);
                }
                else
                {
                    ModelState.AddModelError("", "Error al obtener los usuarios.");
                }
            }
            return View(usuarios);
        }

        // EDITAR ROL - GET
        public async Task<ActionResult> EditarRol(int id)
        {
            using (var client = GetClient())
            {
                // Obtener el usuario por ID
                var userResp = await client.GetAsync($"api/Usuarios/GetUsuariosId/{id}");
                if (!userResp.IsSuccessStatusCode)
                {
                    TempData["Mensaje"] = $"No se pudo obtener el usuario. Código: {userResp.StatusCode}";
                    return RedirectToAction("Index");
                }

                string userJson = await userResp.Content.ReadAsStringAsync();
                var usuario = JsonConvert.DeserializeObject<UsuarioViewModel>(userJson); // ✅

                // Obtener la lista de roles
                var rolesResp = await client.GetAsync("api/Usuarios/GetRoles");
                if (!rolesResp.IsSuccessStatusCode)
                {
                    TempData["Mensaje"] = "No se pudieron cargar los roles.";
                    return RedirectToAction("Index");
                }

                string rolesJson = await rolesResp.Content.ReadAsStringAsync();
                var roles = JsonConvert.DeserializeObject<List<RolViewModel>>(rolesJson); // ✅

                ViewBag.Roles = roles.Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Nombre
                }).ToList();

                // Armar el modelo de la vista
                var model = new CambiarRolViewModel
                {
                    Id = usuario.Id,
                    Nombre = usuario.Nombre,
                    Correo = usuario.Correo,
                    RolActual = usuario.RolNombre,
                };

                return View(model);
            }
        }

        // EDITAR ROL - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditarRol(CambiarRolViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Mensaje"] = "Formulario inválido.";
                return RedirectToAction("Index");
            }

            using (var client = GetClient())
            {
                var data = new { IdNuevoRol = model.NuevoRolId };
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var patch = new HttpMethod("PATCH");
                var request = new HttpRequestMessage(patch, $"api/Usuarios/AsignarRol/{model.Id}")
                {
                    Content = content
                };

                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                    TempData["Mensaje"] = "Rol actualizado correctamente.";
                else
                    TempData["Mensaje"] = "Error al actualizar el rol.";
            }

            return RedirectToAction("Index", new { id = model.Id });
        }

        // ELIMINAR - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Eliminar(int id)
        {
            try
            {
                using (var client = GetClient())
                {
                    HttpResponseMessage resp = await client.DeleteAsync($"api/Usuarios/{id}");
                    if (resp.IsSuccessStatusCode)
                    {
                        TempData["Mensaje"] = "Usuario eliminado correctamente.";
                    }
                    else
                    {
                        TempData["Mensaje"] = "Error al eliminar el usuario.";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error interno: " + ex.Message;
            }

            return RedirectToAction("Index");
        }

        // RECUPERAR CONTRASEÑA - GET
        public ActionResult RecuperarContrasena()
        {
            return View();
        }

        // RECUPERAR CONTRASEÑA - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RecuperarContrasena(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
            {
                TempData["Mensaje"] = "Debe ingresar un correo válido.";
                return View();
            }

            using (var client = GetClient())
            {
                var json = JsonConvert.SerializeObject(new { Correo = correo });
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/Usuarios/RecuperarContrasena", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Mensaje"] = "Correo de recuperación enviado correctamente.";
                }
                else
                {
                    TempData["Mensaje"] = "No se pudo enviar el correo. Verifique el email.";
                }
            }

            return View();
        }

        // REINICIAR CONTRASEÑA - GET
        public ActionResult ReiniciarContrasena()
        {
            return View();
        }

        // REINICIAR CONTRASEÑA - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ReiniciarContrasena(ReiniciarContrasenaViewModel modelo)
        {
            if (!ModelState.IsValid)
                return View(modelo);

            using (var client = GetClient())
            {
                var json = JsonConvert.SerializeObject(modelo);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage resp = await client.PostAsync("api/Usuarios/ReiniciarContrasena", content);
                if (resp.IsSuccessStatusCode)
                {
                    TempData["Mensaje"] = "Contraseña restablecida correctamente.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Mensaje"] = "Error al restablecer la contraseña.";
                }
            }

            return View(modelo);
        }


    }
}
