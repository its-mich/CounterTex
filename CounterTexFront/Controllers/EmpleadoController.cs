using Newtonsoft.Json;
using CounterTexFront.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CounterTexFront.Controllers
{
    public class EmpleadoController : Controller
    {
        string apiUrl = ConfigurationManager.AppSettings["Api"].ToString();

        public async Task<ActionResult> Index()
        {
            List<PerfilEmpleado> empleados = new List<PerfilEmpleado>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                HttpResponseMessage response = await client.GetAsync("api/Empleado/GetEmpleado");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    empleados = JsonConvert.DeserializeObject<List<PerfilEmpleado>>(jsonResponse);
                }
            }
            return View(empleados);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PerfilEmpleado model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                string json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PostAsync("api/Empleado/PostEmpleado", content);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PerfilEmpleado model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                string json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PutAsync("api/Empleado/PutEmpleado", content);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                await client.DeleteAsync($"api/Empleado/DeleteEmpleado/{id}");
            }
            return RedirectToAction("Index");
        }
    }
}
