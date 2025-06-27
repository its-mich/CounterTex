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
    /// <summary>
    /// Controlador para funcionalidades administrativas como gestión de usuarios, roles, metas, producción y horarios.
    /// </summary>
    [Authorize]
    public class AdministradorController : BaseController
    {
        private readonly string apiUrl = ConfigurationManager.AppSettings["Api"].ToString();


        /// <summary>
        /// Crea una instancia de HttpClient configurado con la URL base y token de autorización.
        /// </summary>
        private HttpClient GetClient()
        {
            var client = new HttpClient { BaseAddress = new Uri(apiUrl) };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var token = System.Web.HttpContext.Current.Session["BearerToken"]?.ToString();
            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client;
        }

        /// <summary>
        /// Obtiene y muestra la lista de usuarios desde la API.
        /// </summary>
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

        /// <summary>
        /// Muestra la vista de edición de rol para un usuario específico.
        /// </summary>
        /// <param name="id">ID del usuario a editar</param>
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

        /// <summary>
        /// Procesa el cambio de rol de un usuario.
        /// </summary>
        /// <param name="model">Modelo con los datos del cambio de rol</param>
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

        /// <summary>
        /// Elimina un usuario por su ID.
        /// </summary>
        /// <param name="id">ID del usuario a eliminar</param>
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
                        var errorDetail = await resp.Content.ReadAsStringAsync();
                        TempData["Mensaje"] = $"Error al eliminar el usuario: {resp.StatusCode} - {errorDetail}";

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error interno: " + ex.Message;
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Muestra el formulario para crear una meta.
        /// </summary>
        public ActionResult Crear()
        {
            return View();
        }

        /// <summary>
        /// Procesa el envío del formulario de creación de meta.
        /// </summary>
        /// <param name="model">Modelo de la meta a registrar</param>
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

        /// <summary>
        /// Muestra la vista de producciones diarias con sus detalles, usuarios, prendas y operaciones.
        /// </summary>
        public async Task<ActionResult> ProduccionDiaria()
        {

            List<ProduccionViewModel> listaProduccion = new List<ProduccionViewModel>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);

                try
                {
                    // 1. Obtener producciones
                    HttpResponseMessage response = await client.GetAsync("api/Produccion");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonProduccion = await response.Content.ReadAsStringAsync();
                        listaProduccion = JsonConvert.DeserializeObject<List<ProduccionViewModel>>(jsonProduccion);
                    }

                    // 2. Obtener detalles de producción
                    var detalles = await GetFromApi<List<ProduccionDetalleViewModel>>(client, "api/ProduccionDetalle") ?? new List<ProduccionDetalleViewModel>();

                    // 3. Obtener usuarios, prendas, operaciones
                    var usuarios = await GetFromApi<List<UsuarioViewModel>>(client, "api/Usuarios/GetUsuarios") ?? new List<UsuarioViewModel>();

                    if (usuarios == null || !usuarios.Any())
                    {
                        System.Diagnostics.Debug.WriteLine("❌ No se recibieron usuarios o la lista está vacía.");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("✅ Usuarios cargados desde la API:");
                        foreach (var u in usuarios)
                        {
                            System.Diagnostics.Debug.WriteLine($"Id: {u.Id}, Nombre: {u.Nombre}");
                        }
                    }

                    var prendas = await GetFromApi<List<PrendasEntregadasViewModel>>(client, "api/Prendas") ?? new List<PrendasEntregadasViewModel>();
                    var operaciones = await GetFromApi<List<OperacionViewModel>>(client, "api/Operacion") ?? new List<OperacionViewModel>();

                    // 4. Asignar detalles a cada producción
                    foreach (var prod in listaProduccion)
                    {
                        prod.ProduccionDetalles = detalles.Where(d => d.ProduccionId == prod.Id).ToList();


                        System.Diagnostics.Debug.WriteLine($"Producción ID: {prod.Id} - UsuarioId: {prod.UsuarioId}");

                        var usuario = usuarios.FirstOrDefault(u => u.Id == prod.UsuarioId);

                        if (usuario != null)
                        {
                            System.Diagnostics.Debug.WriteLine($"➡️ Coincidencia encontrada: {usuario.Nombre}");
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("❗UsuarioId no coincide con ningún Id en la lista de usuarios.");
                        }


                        prod.NombreUsuario = usuarios.FirstOrDefault(u => u.Id == prod.UsuarioId)?.Nombre ?? "Desconocido";
                        prod.NombrePrenda = prendas.FirstOrDefault(p => p.Id == prod.PrendaId)?.Nombre ?? "Sin nombre";

                        foreach (var detalle in prod.ProduccionDetalles)
                        {
                            var operacion = operaciones.FirstOrDefault(o => o.Id == detalle.OperacionId);
                            if (operacion != null)
                            {
                                detalle.NombreOperacion = operacion.Nombre;
                                detalle.ValorOperacion = operacion.ValorUnitario;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Error al cargar los datos de producción: " + ex.Message;
                }
            }

            return View(listaProduccion);
        }


        // <summary>
        /// Muestra la vista de control de horarios de empleados.
        /// </summary>
        public async Task<ActionResult> ControlHorarios()
        {
            List<HorarioViewModel> horarios = new List<HorarioViewModel>();

            try
            {
                using (var client = GetClient())
                {
                    var response = await client.GetAsync("api/Usuarios/GetUsuarios");
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var usuarios = JsonConvert.DeserializeObject<List<UsuarioViewModel>>(json);

                        ViewBag.Usuarios = usuarios;
                        ViewBag.EmpleadosJson = JsonConvert.SerializeObject(usuarios);
                        return View(new List<HorarioViewModel>());
                    }

                    ViewBag.Usuarios = new List<UsuarioViewModel>();
                    ViewBag.EmpleadosJson = "[]";
                    TempData["Error"] = "No se pudieron obtener los empleados.";
                    return View(new List<HorarioViewModel>());
                }
            }
            catch (Exception ex)
            {
                ViewBag.Usuarios = new List<UsuarioViewModel>();
                ViewBag.EmpleadosJson = "[]";
                TempData["Error"] = $"Error inesperado: {ex.Message}";
                return View(new List<HorarioViewModel>());
            }
        }

        /// <summary>
        /// Registra un nuevo horario para un empleado.
        /// </summary>
        /// <param name="model">Datos del nuevo horario</param>
        [HttpPost]
        public async Task<ActionResult> CreateHorario(HorarioViewModel model)
        {
            try
            {
                using (var client = GetClient())
                {
                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("api/Horarios/PostHorario", content);

                    if (!response.IsSuccessStatusCode)
                        return Json(new { exito = false, mensaje = "Error al crear el registro." });

                    return Json(new { exito = true, mensaje = "Registro creado correctamente." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { exito = false, mensaje = "Error: " + ex.Message });
            }
        }
    }
}
