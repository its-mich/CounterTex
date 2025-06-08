using Newtonsoft.Json;
using CounterTexFront.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CounterTexFront.Controllers
{
    [Authorize]
    public class AdministradorController : BaseController
    {
        private readonly string apiUrl = ConfigurationManager.AppSettings["Api"];

        // Mostrar lista de usuarios
        public async Task<ActionResult> Index()
        {
            List<UsuarioViewModel> usuarios = new List<UsuarioViewModel>();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);

                    // ⚠️ Token de sesión, si lo estás usando para la API
                    var token = Session["BearerToken"]?.ToString();
                    if (!string.IsNullOrEmpty(token))
                    {
                        client.DefaultRequestHeaders.Authorization =
                            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    }

                    var response = await client.GetAsync("api/Usuarios/GetUsuarios");

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        usuarios = JsonConvert.DeserializeObject<List<UsuarioViewModel>>(json);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        // 🔐 Token inválido o expirado → cerrar sesión
                        Session.Clear();
                        Session.Abandon();
                        return RedirectToAction("Login", "Auth");
                    }
                    else
                    {
                        ModelState.AddModelError("", $"Error al obtener usuarios. Código: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error inesperado: " + ex.Message);
            }

            return View("Index", usuarios);
        }

        // GET: Administrador/EditarUsuario/5
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> EditarUsuario(int id)
        {
            UsuarioViewModel usuario = null;
            List<RolViewModel> roles = new List<RolViewModel>();

            using (var client = new HttpClient { BaseAddress = new Uri(apiUrl) })
            {
                var responseUsuario = await client.GetAsync($"api/Usuarios/{id}");
                var responseRoles = await client.GetAsync("api/Roles");

                if (responseUsuario.IsSuccessStatusCode)
                {
                    var json = await responseUsuario.Content.ReadAsStringAsync();
                    usuario = JsonConvert.DeserializeObject<UsuarioViewModel>(json);
                }

                if (responseRoles.IsSuccessStatusCode)
                {
                    var jsonRoles = await responseRoles.Content.ReadAsStringAsync();
                    roles = JsonConvert.DeserializeObject<List<RolViewModel>>(jsonRoles);
                }
            }

            if (usuario == null)
                return HttpNotFound();

            ViewBag.Roles = roles;
            return View(usuario);
        }

        // POST: Administrador/EditarRolUsuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> EditarRolUsuario(int id, int rolId)
        {
            if (Session["BearerToken"] == null)
            {
                return RedirectToAction("Login", "Auth"); // ← redirige al login personalizado si no hay token
            }
            var token = Session["BearerToken"].ToString();

            if (rolId <= 0)
            {
                ModelState.AddModelError("", "Debe seleccionar un rol válido.");
                return RedirectToAction("EditarUsuario", new { id });
            }

            using (var client = new HttpClient { BaseAddress = new Uri(apiUrl) })
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var payload = new StringContent(JsonConvert.SerializeObject(rolId), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"api/Usuarios/AsignarRol/{id}")
                {
                    Content = payload
                };

                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Mensaje"] = "Rol actualizado correctamente.";
                    return RedirectToAction("Index");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["Mensaje"] = "Sesión expirada. Por favor vuelve a iniciar sesión.";
                    return RedirectToAction("Login", "Auth");
                }
                else
                {
                    ModelState.AddModelError("", "Error al actualizar el rol. Código: " + response.StatusCode);
                    return RedirectToAction("EditarUsuario", new { id });
                }
            }
        }

        // Eliminar usuario
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> EliminarUsuario(int id)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(apiUrl) })
            {
                var response = await client.DeleteAsync($"api/Usuarios/{id}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["Mensaje"] = "Usuario eliminado correctamente.";
                }
                else
                {
                    TempData["Mensaje"] = $"No se pudo eliminar el usuario. Código: {response.StatusCode}";
                }
            }

            return RedirectToAction("Index");
        }
    }
}
