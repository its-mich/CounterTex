using CounterTexFront.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CounterTexFront.Controllers
{
    public class ControlHorariosController : BaseController
    {
        string apiUrl = ConfigurationManager.AppSettings["Api"];

        public ControlHorariosController()
        {
            if (string.IsNullOrEmpty(apiUrl))
                throw new InvalidOperationException("No se encontró la configuración 'Api' en Web.config.");
        }
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

                        // Filtrar solo los del empleado logeado
                        var registros = todosLosRegistros
                            .Where(r => r.EmpleadoId == empleadoActual.Id)
                            .ToList();

                        // ✅ Asignar el nombre del empleado manualmente
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


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        

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
