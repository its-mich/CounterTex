using CounterTexFront.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CounterTexFront.Controllers
{
    /// <summary>
    /// Controlador que gestiona el control de horarios de empleados.
    /// Permite visualizar y registrar entradas y salidas del día actual.
    /// </summary>
    public class ControlHorariosController : BaseController
    {
        private readonly string apiUrl = ConfigurationManager.AppSettings["Api"];

        /// <summary>
        /// Constructor que valida la configuración de la URL de la API.
        /// </summary>
        /// <exception cref="InvalidOperationException">Si la configuración 'Api' no está definida.</exception>
        public ControlHorariosController()
        {
            if (string.IsNullOrEmpty(apiUrl))
                throw new InvalidOperationException("No se encontró la configuración 'Api' en Web.config.");
        }

        /// <summary>
        /// Muestra la vista principal con los horarios del empleado logeado.
        /// También calcula estadísticas del día (presentes, ausentes, atrasos).
        /// </summary>
        /// <returns>Vista con la lista de registros <see cref="HorarioViewModel"/></returns>
        public async Task<ActionResult> Index()
        {
            try
            {
                string fechaActual = DateTime.Today.ToString("yyyy-MM-dd");
                var empleadoActual = await ObtenerEmpleadoLogeado();

                if (empleadoActual == null)
                {
                    ViewBag.MensajeError = "No se pudo obtener la información del empleado.";
                    return View(new List<HorarioViewModel>());
                }

                ViewBag.EmpleadoActual = empleadoActual;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    var response = await client.GetAsync($"api/Horarios/GetHorariosPorFecha?fecha={fechaActual}");

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var todosLosRegistros = JsonConvert.DeserializeObject<List<HorarioViewModel>>(json);

                        var registros = todosLosRegistros
                            .Where(r => r.EmpleadoId == empleadoActual.Id)
                            .ToList();

                        foreach (var r in registros)
                        {
                            r.EmpleadoNombre = empleadoActual.Nombre;
                        }

                        // Estadísticas
                        ViewBag.TotalEmpleados = 1;
                        ViewBag.PresentesHoy = registros.Any(r => r.Tipo.ToLower() == "entrada") ? 1 : 0;
                        ViewBag.AusentesHoy = ViewBag.TotalEmpleados - ViewBag.PresentesHoy;
                        ViewBag.AtrasadosHoy = registros.Count(r => r.Tipo.ToLower() == "entrada" && r.Hora > new TimeSpan(8, 0, 0));

                        return View(registros);
                    }

                    ViewBag.MensajeError = "No se pudieron cargar los registros.";
                    return View(new List<HorarioViewModel>());
                }
            }
            catch (Exception ex)
            {
                ViewBag.MensajeError = "Error: " + ex.Message;
                return View(new List<HorarioViewModel>());
            }
        }

        /// <summary>
        /// Permite registrar manualmente un horario mediante un POST desde formulario.
        /// </summary>
        /// <param name="model">Datos del horario a registrar</param>
        /// <returns>Redirección a la vista Index</returns>
        [HttpPost]
        public async Task<ActionResult> RegistrarHorarioManual(HorarioViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/Horarios/PostHorario", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["MensajeExito"] = "Registro guardado correctamente.";
                }
                else
                {
                    TempData["MensajeError"] = "Ocurrió un error al guardar el registro.";
                }

                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Obtiene los datos del empleado actualmente logeado a través de una cookie.
        /// </summary>
        /// <returns>Instancia de <see cref="PerfilEmpleadoViewModel"/> o null</returns>
        private async Task<PerfilEmpleadoViewModel> ObtenerEmpleadoLogeado()
        {
            var empleadoIdCookie = Request.Cookies["EmpleadoId"]?.Value;
            if (string.IsNullOrEmpty(empleadoIdCookie))
                return null;

            int empleadoId = int.Parse(empleadoIdCookie);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                var response = await client.GetAsync($"api/Usuarios/GetUsuariosId/{empleadoId}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var usuario = JsonConvert.DeserializeObject<PerfilEmpleadoViewModel>(json);
                    return usuario;
                }
            }

            return null;
        }

        /// <summary>
        /// Devuelve la vista vacía para crear un nuevo registro de horario.
        /// </summary>
        /// <returns>Vista vacía para nuevo horario</returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Crea un nuevo registro de horario a través de una petición AJAX POST.
        /// </summary>
        /// <param name="model">Horario a registrar</param>
        /// <returns>Resultado en formato JSON indicando éxito o error</returns>
        [HttpPost]
        public async Task<ActionResult> Create(HorarioViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { exito = false, mensaje = "Datos inválidos." });

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("api/Horarios/PostHorario", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { exito = true, mensaje = "Registro guardado correctamente." });
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        return Json(new { exito = false, mensaje = "Error en la API: " + errorContent });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { exito = false, mensaje = "Excepción: " + ex.Message });
            }
        }
    }
}
