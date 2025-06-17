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
            var empleados = await ObtenerEmpleados();
            ViewBag.Empleados = empleados;
            ViewBag.EmpleadosJson = JsonConvert.SerializeObject(empleados); // ← ESTO FALTABA
            return View();
        }

        private async Task<List<PerfilEmpleadoViewModel>> ObtenerEmpleados()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                var response = await client.GetAsync("api/usuarios");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var usuarios = JsonConvert.DeserializeObject<List<PerfilEmpleadoViewModel>>(json);

                    // Suponiendo que tienes un campo Tipo o Rol
                    return usuarios.Where(u => u.Rol == "empleado").ToList();
                }
            }


            return new List<PerfilEmpleadoViewModel>();
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
                    string fechaActual = DateTime.Today.ToString("yyyy-MM-dd");

                    var response = await client.GetAsync($"api/horarios?fecha={fechaActual}");

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var registros = JsonConvert.DeserializeObject<List<HorarioViewModel>>(json);
                        return Json(registros, JsonRequestBehavior.AllowGet);
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
    }
}
