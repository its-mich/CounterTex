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
    public class ResumenProduccionController : BaseController
    {
        private readonly string apiUrl = ConfigurationManager.AppSettings["Api"];

        public async Task<ActionResult> Index()
        {
            List<UsuarioViewModel> empleados = new List<UsuarioViewModel>();
            List<string> tiposPrenda = new List<string>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);

                    // Obtener empleados desde la API
                    var responseEmpleados = await client.GetAsync("api/Usuarios/GetUsuarios");
                    if (responseEmpleados.IsSuccessStatusCode)
                    {
                        var json = await responseEmpleados.Content.ReadAsStringAsync();
                        var todos = JsonConvert.DeserializeObject<List<UsuarioViewModel>>(json);
                        empleados = todos.Where(u => u.RolNombre == "Empleado").ToList();
                    }
                    else
                    {
                        ViewBag.Error = "No se pudieron cargar los empleados.";
                    }

                    // Obtener tipos de prenda desde la API
                    var responsePrendas = await client.GetAsync("api/Produccion/GetTiposPrenda");
                    if (responsePrendas.IsSuccessStatusCode)
                    {
                        var jsonTipos = await responsePrendas.Content.ReadAsStringAsync();
                        tiposPrenda = JsonConvert.DeserializeObject<List<string>>(jsonTipos);
                    }
                    else
                    {
                        ViewBag.Error = "No se pudieron cargar los tipos de prenda.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            ViewBag.Empleados = empleados;
            ViewBag.TiposPrenda = tiposPrenda;
            ViewBag.Layout = Session["Layout"];
            return View();
        }
    }
}
