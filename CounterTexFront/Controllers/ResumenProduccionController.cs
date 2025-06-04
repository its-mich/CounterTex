using System;
using System.Collections.Generic;
using System.Configuration;
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

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    HttpResponseMessage response = await client.GetAsync("api/Usuarios");

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        empleados = JsonConvert.DeserializeObject<List<UsuarioViewModel>>(json);
                    }
                    else
                    {
                        ViewBag.Error = "No se pudo cargar la lista de empleados.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            }

            ViewBag.Empleados = empleados;
            ViewBag.Layout = Session["Layout"];
            return View();
        }
    }
}