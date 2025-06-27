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
    /// <summary>
    /// Controlador encargado de gestionar la vista y acciones relacionadas con los empleados.
    /// Incluye operaciones CRUD que se comunican con la API externa.
    /// </summary>
    public class EmpleadoController : BaseController
    {
        private string apiUrl = ConfigurationManager.AppSettings["Api"].ToString();

        /// <summary>
        /// Muestra la lista de empleados obtenida desde la API.
        /// </summary>
        /// <returns>Vista con la lista de empleados.</returns>
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

            return View(empleados);
        }

        /// <summary>
        /// Crea un nuevo empleado enviando los datos a la API.
        /// </summary>
        /// <param name="model">Datos del empleado a registrar.</param>
        /// <returns>Redirecciona al índice si tiene éxito; de lo contrario, devuelve la vista con errores.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PerfilEmpleadoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

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
                        return View(model);
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al crear el empleado: " + ex.Message);
                return View(model);
            }
        }

        /// <summary>
        /// Edita los datos de un empleado existente.
        /// </summary>
        /// <param name="model">Datos actualizados del empleado.</param>
        /// <returns>Redirecciona al índice si tiene éxito; de lo contrario, devuelve la vista con errores.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PerfilEmpleadoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

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
                        return View(model);
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al editar el empleado: " + ex.Message);
                return View(model);
            }
        }

        /// <summary>
        /// Elimina un empleado por su ID usando la API.
        /// </summary>
        /// <param name="id">ID del empleado a eliminar.</param>
        /// <returns>Redirecciona al índice con resultado de la operación.</returns>
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
