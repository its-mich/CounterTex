using System;
using System.Collections.Generic;
using System.Configuration;
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

        // GET: ProduccionDiaria
        public async Task<ActionResult> Index()
        {
            List<ProduccionDiariaViewModel> producciones = new List<ProduccionDiariaViewModel>();

            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync($"{apiUrl}/Produccion");
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        producciones = JsonConvert.DeserializeObject<List<ProduccionDiariaViewModel>>(json);
                    }
                    else
                    {
                        ViewBag.Error = $"Error: {response.StatusCode}";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Excepción: " + ex.Message;
                }
            }

            return View(producciones);
        }

        // GET: ProduccionDiaria/Create
        public ActionResult Create()
        {
            var model = new ProduccionDiariaViewModel
            {
                Fecha = DateTime.Today,
                Detalles = new List<ProduccionDetalleViewModel>
                {
                    new ProduccionDetalleViewModel()
                }
            };

            return View(model);
        }

        // POST: ProduccionDiaria/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProduccionDiariaViewModel model)
        {
            if (model.Detalles == null || model.Detalles.Count == 0)
            {
                model.Detalles = new List<ProduccionDetalleViewModel>
                {
                    new ProduccionDetalleViewModel()
                };
            }

            try
            {
                using (var client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync($"{apiUrl}/Produccion", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        string resp = await response.Content.ReadAsStringAsync();
                        ViewBag.Error = $"Error al guardar: {response.StatusCode} — {resp}";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Excepción: " + ex.Message;
            }

            return View(model);
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
                        ViewBag.Error = $"Error al eliminar: {response.StatusCode}";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Excepción al eliminar: " + ex.Message;
                }
            }

            return RedirectToAction("Index");
        }
    }
}
