using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CounterTexFront.Models;
using Newtonsoft.Json;

namespace CounterTexFront.Controllers
{
    public class ProduccionDiariaController : BaseController
    {
        private readonly string apiUrl = ConfigurationManager.AppSettings["Api"];

        // Helper para cargar datos de DropDownLists
        private async Task<ProduccionDiariaViewModel> LoadFormData(ProduccionDiariaViewModel model)
        {
            using (var client = new HttpClient())
            {
                var usuariosResponse = await client.GetAsync($"{apiUrl}/Usuarios");
                model.UsuariosDisponibles = usuariosResponse.IsSuccessStatusCode
                    ? JsonConvert.DeserializeObject<List<UsuarioViewModel>>(await usuariosResponse.Content.ReadAsStringAsync())
                    : new List<UsuarioViewModel>();

                var prendasResponse = await client.GetAsync($"{apiUrl}/Prendas");
                model.PrendasDisponibles = prendasResponse.IsSuccessStatusCode
                    ? JsonConvert.DeserializeObject<List<PrendasEntregadasViewModel>>(await prendasResponse.Content.ReadAsStringAsync())
                    : new List<PrendasEntregadasViewModel>();

                var operacionesResponse = await client.GetAsync($"{apiUrl}/Operacion");
                model.OperacionesDisponibles = operacionesResponse.IsSuccessStatusCode
                    ? JsonConvert.DeserializeObject<List<OperacionViewModel>>(await operacionesResponse.Content.ReadAsStringAsync())
                    : new List<OperacionViewModel>();
            }
            return model;
        }

        // GET: ProduccionDiaria (Para la lista de producciones con paginación)
        public async Task<ActionResult> Index(int page = 1) // Añadir parámetro de página
        {
            List<ProduccionDtoViewModel> produccionesCompletas = new List<ProduccionDtoViewModel>();
            int pageSize = 10; // Define el tamaño de página, puedes hacerlo configurable

            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync($"{apiUrl}/Produccion");
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        produccionesCompletas = JsonConvert.DeserializeObject<List<ProduccionDtoViewModel>>(json);

                        int totalRegistros = produccionesCompletas.Count;
                        int totalPaginas = (int)Math.Ceiling((double)totalRegistros / pageSize);

                        var produccionesPaginadas = produccionesCompletas.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                        var model = new ProduccionListViewModel
                        {
                            ListaProducciones = produccionesPaginadas,
                            PaginaActual = page,
                            TotalPaginas = totalPaginas,
                            TamanoPagina = pageSize,
                            TotalRegistros = totalRegistros
                        };
                        return View(model);
                    }
                    else
                    {
                        string errorContent = await response.Content.ReadAsStringAsync();
                        ViewBag.Error = $"Error al cargar producciones: {response.StatusCode} - {errorContent}";
                        return View(new ProduccionListViewModel { ListaProducciones = new List<ProduccionDtoViewModel>(), PaginaActual = page, TotalPaginas = 1 });
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Excepción al cargar producciones: " + ex.Message;
                    return View(new ProduccionListViewModel { ListaProducciones = new List<ProduccionDtoViewModel>(), PaginaActual = page, TotalPaginas = 1 });
                }
            }
        }

        // GET: ProduccionDiaria/Create
        public async Task<ActionResult> Create()
        {
            var model = new ProduccionDiariaViewModel
            {
                Fecha = DateTime.Today,
                ProduccionDetalles = new List<ProduccionDetalleViewModel>()
            };
            model = await LoadFormData(model);
            return View(model);
        }

        // POST: ProduccionDiaria/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProduccionDiariaViewModel model)
        {
            // Aseguramos que ProduccionDetalles no sea null
            if (model.ProduccionDetalles == null)
            {
                model.ProduccionDetalles = new List<ProduccionDetalleViewModel>();
            }

            model = await LoadFormData(model);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.ProduccionDetalles == null || !model.ProduccionDetalles.Any())
            {
                ModelState.AddModelError("ProduccionDetalles", "Debe agregar al menos un detalle de producción.");
                return View(model);
            }

            model.ProduccionDetalles.RemoveAll(d => d.Cantidad <= 0 || d.OperacionId <= 0);

            if (!model.ProduccionDetalles.Any())
            {
                ModelState.AddModelError("ProduccionDetalles", "Ningún detalle de producción es válido. Verifique las cantidades y operaciones.");
                return View(model);
            }

            try
            {
                var produccionParaApi = new ProduccionCreateDto
                {
                    Fecha = model.Fecha,
                    UsuarioId = model.UsuarioId,
                    PrendaId = model.PrendaId,
                    ProduccionDetalles = model.ProduccionDetalles.Select(d => new ProduccionDetalleCreateDto
                    {
                        Cantidad = d.Cantidad,
                        OperacionId = d.OperacionId
                    }).ToList()
                };

                using (var client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(produccionParaApi);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync($"{apiUrl}/Produccion", content);

                    if (response.IsSuccessStatusCode)
                    {
                        // ¡CAMBIO AQUÍ! Añadir mensaje de éxito a TempData
                        TempData["SuccessMessage"] = "Producción guardada exitosamente.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        string resp = await response.Content.ReadAsStringAsync();
                        try
                        {
                            var errorObj = JsonConvert.DeserializeObject<dynamic>(resp);
                            ViewBag.Error = $"Error al guardar: {response.StatusCode} - {errorObj?.title ?? errorObj?.message ?? resp}";
                        }
                        catch
                        {
                            ViewBag.Error = $"Error al guardar: {response.StatusCode} - {resp}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Excepción al guardar la producción: " + ex.Message;
            }

            return View(model);
        }

        // GET: ProduccionDiaria/Details/5
        public async Task<ActionResult> Details(int id)
        {
            ProduccionDiariaViewModel produccion = null;

            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync($"{apiUrl}/Produccion/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        produccion = JsonConvert.DeserializeObject<ProduccionDiariaViewModel>(json);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        return HttpNotFound($"No se encontró la producción con ID: {id}");
                    }
                    else
                    {
                        string errorContent = await response.Content.ReadAsStringAsync();
                        ViewBag.Error = $"Error al cargar los detalles de la producción: {response.StatusCode} - {errorContent}";
                        return View("Error");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Excepción al cargar los detalles de la producción: " + ex.Message;
                    return View("Error");
                }
            }

            if (produccion == null)
            {
                return HttpNotFound($"La producción con ID: {id} no pudo ser cargada o no existe.");
            }

            return View(produccion);
        }

        // GET: ProduccionDiaria/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.DeleteAsync($"{apiUrl}/Produccion/{id}");
                    if (!response.IsSuccessStatusCode)
                    {
                        string errorContent = await response.Content.ReadAsStringAsync();
                        ViewBag.Error = $"Error al eliminar: {response.StatusCode} - {errorContent}";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Excepción al eliminar: " + ex.Message;
                }
            }
            return RedirectToAction("Index");
        }

        // ACCIÓN: Para cargar la vista parcial de un detalle de producción dinámicamente
        public async Task<ActionResult> GetProduccionDetallePartial(int index)
        {
            var model = new ProduccionDetalleViewModel();
            IEnumerable<OperacionViewModel> operaciones = new List<OperacionViewModel>();

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{apiUrl}/Operacion");
                if (response.IsSuccessStatusCode)
                {
                    operaciones = JsonConvert.DeserializeObject<List<OperacionViewModel>>(await response.Content.ReadAsStringAsync());
                }
            }

            ViewData["index"] = index;
            ViewData["operaciones"] = operaciones;

            return PartialView("_ProduccionDetallePartial", model);
        }
    }
}