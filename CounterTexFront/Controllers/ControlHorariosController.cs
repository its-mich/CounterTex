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
        //public async Task<ActionResult> Index()
        //{
        //    var empleados = await ObtenerEmpleadoActual();
        //    ViewBag.Empleados = empleados;
        //    ViewBag.EmpleadosJson = JsonConvert.SerializeObject(empleados); // ← ESTO FALTABA
        //    return View();
        //}

        private async Task<PerfilEmpleadoViewModel> ObtenerEmpleadoActual()
        {
            var usuarioId = Session["UsuarioId"]?.ToString(); // O usa ViewBag.UsuarioId, TempData, etc.

            if (string.IsNullOrEmpty(usuarioId))
                return null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);

                var response = await client.GetAsync($"api/Usuarios/GetUsuarioPorId/{usuarioId}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var usuario = JsonConvert.DeserializeObject<PerfilEmpleadoViewModel>(json);

                    // Validar que sea empleado
                    if (usuario != null && usuario.Rol == "empleado")
                    {
                        return usuario;
                    }
                }
            }

            return null;
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> ObtenerRegistrosHoy()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);

                    var response = await client.GetAsync("api/Horarios/GetHorarios"); // sin filtro, trae todos

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var todosLosRegistros = JsonConvert.DeserializeObject<List<HorarioViewModel>>(json);

                        // Filtrar por fecha actual (solo la parte de la fecha, ignorando la hora)
                        var fechaActual = DateTime.Today;
                        var registrosDeHoy = todosLosRegistros
                            .Where(r => r.Fecha.Date == fechaActual)
                            .ToList();

                        return Json(registrosDeHoy, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { exito = false, mensaje = "Error al obtener los registros." }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { exito = false, mensaje = ex.Message }, JsonRequestBehavior.AllowGet);
            }
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

                    var response = await client.PostAsync("api/horarios", content);

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

        public async Task<ActionResult> Index()
        {

            if (Session["EmpleadoId"] == null)
            {
                ViewBag.Error = "Empleado no autenticado.";
                return View(new List<HorarioViewModel>());
            }

            int empleadoId = (int)Session["EmpleadoId"];

            //if (empleadoId == 0)
            //{
            //    return RedirectToAction("Login", "Auth");
            //}

            List<HorarioViewModel> horarios = new List<HorarioViewModel>();

            using (var client = new HttpClient())
            {
                string endpoint = $"{apiUrl}/api/Horarios?empleadoId={empleadoId}";



                try
                {
                    HttpResponseMessage response = await client.GetAsync(endpoint);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        ViewBag.RawJson = json; // ← para mostrarlo en la vista y depurar
                        horarios = JsonConvert.DeserializeObject<List<HorarioViewModel>>(json);

                    }
                    else
                    {
                        string errorDetail = await response.Content.ReadAsStringAsync();
                        ViewBag.Error = $"No se pudieron obtener los horarios. Código: {response.StatusCode}. Detalle: {errorDetail}";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Error al conectar con la API: " + ex.Message;
                }


            }

            return View(horarios);
        }
    }
}
