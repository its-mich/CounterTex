using Newtonsoft.Json;
using CounterTexFront.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace CounterTexFront.Controllers
{

    [Authorize]
    public class AdministradorController : BaseController
    {
        private readonly HttpClient _httpClient;
        private readonly string apiUrl = ConfigurationManager.AppSettings["Api"];

        public AdministradorController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(apiUrl)
            };

            var token = System.Web.HttpContext.Current.Session["BearerToken"]?.ToString();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        public ActionResult PruebaRuta()
        {
            return Content("MVC funciona correctamente");
        }


        // Mostrar lista de usuarios
        public async Task<ActionResult> Index()
        {
            List<UsuarioViewModel> usuarios = new List<UsuarioViewModel>();

            try
            {
                var response = await _httpClient.GetAsync("api/Usuarios/GetUsuarios");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    usuarios = JsonConvert.DeserializeObject<List<UsuarioViewModel>>(json);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Session.Clear();
                    Session.Abandon();
                    TempData["Mensaje"] = "Sesión expirada. Por favor, inicia sesión nuevamente.";
                    return RedirectToAction("Login", "Cuenta");
                }
                else
                {
                    ModelState.AddModelError("", $"Error al obtener usuarios. Código: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error inesperado: " + ex.Message);
            }

            return View("Index", usuarios);
        }

        // GET: Admin → lista usuarios
        public async Task<ActionResult> CambiarRol()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Usuarios");
                if (!response.IsSuccessStatusCode)
                {
                    TempData["Mensaje"] = "No se pudieron obtener los usuarios.";
                    return RedirectToAction("Index");
                }

                var list = JsonConvert.DeserializeObject<List<UsuarioViewModel>>(await response.Content.ReadAsStringAsync());
                return View(list);
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error al cargar la vista: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // GET: EditarRol
        public async Task<ActionResult> EditarRol(int id)
        {
            try
            {
                var respU = await _httpClient.GetAsync($"api/Usuarios/{id}");
                var usuario = JsonConvert.DeserializeObject<UsuarioViewModel>(await respU.Content.ReadAsStringAsync());

                var respR = await _httpClient.GetAsync("api/Roles");
                var roles = JsonConvert.DeserializeObject<List<RolViewModel>>(await respR.Content.ReadAsStringAsync());

                var model = new CambiarRolViewModel
                {
                    Id = usuario.Id,
                    Nombre = usuario.Nombre,
                    NombreRol = usuario.RolNombre,
                    RolesDisponibles = roles
                };

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error al obtener datos del usuario: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // POST: EditarRol
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditarRol(CambiarRolViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.RolesDisponibles = JsonConvert.DeserializeObject<List<RolViewModel>>(
                    await (await _httpClient.GetAsync("api/Roles")).Content.ReadAsStringAsync());
                    return View(model);
            }

            try
            {
                var payload = new
                {
                    Id = model.Id,
                    RolId = model.NuevoRolId
                };

                var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                var resp = await _httpClient.PutAsync($"api/Usuarios/{model.Id}/Rol", content);

                TempData[resp.IsSuccessStatusCode ? "SuccessMessage" : "Error"] =
                    resp.IsSuccessStatusCode ? "Rol actualizado correctamente." : "Error al actualizar el rol.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error inesperado al actualizar el rol: " + ex.Message;
            }

            return RedirectToAction("CambiarRol");
        }

        // POST: Eliminar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EliminarUsuario(int id)
        {
            try
            {
                var resp = await _httpClient.DeleteAsync($"api/Usuarios/{id}");
                TempData[resp.IsSuccessStatusCode ? "SuccessMessage" : "Error"] =
                    resp.IsSuccessStatusCode ? "Usuario eliminado correctamente." : "Error al eliminar.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error inesperado al eliminar el usuario: " + ex.Message;
            }

            return RedirectToAction("CambiarRol");
        }
    }
}