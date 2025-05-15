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

namespace CounterTexFront.Controllers
{
    public class EmpleadoController : BaseController
    {
        string apiUrl = ConfigurationManager.AppSettings["Api"].ToString();

        public async Task<ActionResult> Index()
        {
            List<PerfilEmpleadoViewModel> empleados = new List<PerfilEmpleadoViewModel>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    HttpResponseMessage response = await client.GetAsync("api/Empleado/GetEmpleado");

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        empleados = JsonConvert.DeserializeObject<List<PerfilEmpleadoViewModel>>(jsonResponse);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error al obtener los datos de empleados.");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al conectarse con el servidor: " + ex.Message);
            }

            return View(empleados); // Cambio aquí para la vista "Empleado"
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PerfilEmpleadoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model); // Cambio aquí para la vista "Empleado"

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    string json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("api/Empleado/PostEmpleado", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("", "No se pudo crear el empleado. Inténtalo nuevamente.");
                        return View(model); // Cambio aquí para la vista "Empleado"
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al crear el empleado: " + ex.Message);
                return View(model); // Cambio aquí para la vista "Empleado"
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PerfilEmpleadoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model); // Cambio aquí para la vista "Empleado"

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    string json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync("api/Empleado/PutEmpleado", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("", "No se pudo editar el empleado. Inténtalo nuevamente.");
                        return View(model); // Cambio aquí para la vista "Empleado"
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al editar el empleado: " + ex.Message);
                return View(model); // Cambio aquí para la vista "Empleado"
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    HttpResponseMessage response = await client.DeleteAsync($"api/Empleado/DeleteEmpleado/{id}");

                    if (!response.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("", "No se pudo eliminar el empleado. Inténtalo nuevamente.");
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar el empleado: " + ex.Message);
                return RedirectToAction("Index");
            }
        }
    }
}
