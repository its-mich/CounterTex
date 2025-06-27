using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using CounterTexFront.Models;
using Newtonsoft.Json;

namespace CounterTexFront.Controllers
{
    /// <summary>
    /// Controlador que muestra el resumen de producción por empleado y tipo de prenda.
    /// </summary>
    public class ResumenProduccionController : BaseController
    {
        private readonly string apiUrl = ConfigurationManager.AppSettings["Api"];

        /// <summary>
        /// Vista principal con los filtros de resumen de producción.
        /// </summary>
        public async Task<ActionResult> Index()
        {
            List<UsuarioViewModel> empleados = new List<UsuarioViewModel>();
            List<string> tiposPrenda = new List<string>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);

                    // ✅ Obtener empleados desde la API
                    HttpResponseMessage responseEmpleados = await client.GetAsync("api/Usuarios/GetUsuarios");
                    if (responseEmpleados.IsSuccessStatusCode)
                    {
                        string json = await responseEmpleados.Content.ReadAsStringAsync();
                        var todos = JsonConvert.DeserializeObject<List<UsuarioViewModel>>(json);
                        empleados = todos.Where(u => u.RolNombre == "Empleado").ToList();
                    }
                    else
                    {
                        ViewBag.Error = "⚠️ No se pudieron cargar los empleados.";
                    }

                    // ✅ Obtener tipos de prenda desde la API
                    HttpResponseMessage responsePrendas = await client.GetAsync("api/Produccion/GetTiposPrenda");
                    if (responsePrendas.IsSuccessStatusCode)
                    {
                        string jsonTipos = await responsePrendas.Content.ReadAsStringAsync();
                        tiposPrenda = JsonConvert.DeserializeObject<List<string>>(jsonTipos);
                    }
                    else
                    {
                        ViewBag.Error = "⚠️ No se pudieron cargar los tipos de prenda.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "❌ Error al conectar con la API: " + ex.Message;
            }

            // ✅ Pasamos datos a la vista
            ViewBag.Empleados = empleados;
            ViewBag.TiposPrenda = tiposPrenda;
            ViewBag.Layout = Session["Layout"];

            return View(); // ← Esto carga Index.cshtml dentro de Views/ResumenProduccion/
        }
    }
}
