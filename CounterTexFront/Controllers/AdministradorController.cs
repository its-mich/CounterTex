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
        [AllowAnonymous]
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
            try
            {
                using (var client = new HttpClient())
                {
                    // Establece la URL base de la API
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Clear();

                    // Serializa el modelo a formato JSON
                    string json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    // Envía la solicitud POST al microservicio de usuarios
                    HttpResponseMessage response = await client.PostAsync("api/Metas/PostMetas", content);

                    // Verifica si la respuesta fue exitosa
                    if (response.IsSuccessStatusCode)
                    {
                        var res = await response.Content.ReadAsStringAsync();

                        // ✅ Mensaje de éxito
                        TempData["SuccessMessage"] = "La meta se guardó exitosamente.";

                        return RedirectToAction("Crear", "Administrador");
                    }

                    else
                    {
                        // Captura errores y los guarda en TempData para mostrarlos en la vista
                        var errorContent = await response.Content.ReadAsStringAsync();
                        TempData["Error"] = $"Error de API: {response.StatusCode} - {errorContent}";
                        return RedirectToAction("Crear", "Administrador");
                    }
                }

                // Redirige al usuario a la vista principal si todo fue exitoso
                return RedirectToAction("Index", "Meta");
            }
            catch (Exception ex)
            {
                // Maneja cualquier excepción y muestra un mensaje de error
                TempData["Error"] = $"Hubo un error al procesar la solicitud: {ex.Message}";
                return RedirectToAction("Crear", "Administrador");
            }
        }

       
    }
}
