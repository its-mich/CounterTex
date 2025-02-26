using CounterTexFront.Models;
using Newtonsoft.Json;
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
    public class AdministradorController : Controller
    {
        string apiUrl = ConfigurationManager.AppSettings["Api"].ToString();

        public async Task<ActionResult> Index()
        {
            List<PerfilAdministrador> administradores = new List<PerfilAdministrador>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                HttpResponseMessage response = await client.GetAsync("api/Administrador/GetAdministrador");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    administradores = JsonConvert.DeserializeObject<List<PerfilAdministrador>>(jsonResponse);
                }
            }
            return View(administradores);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PerfilAdministrador model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                string json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PostAsync("api/Administrador/PostAdministrador", content);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PerfilAdministrador model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                string json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PutAsync("api/Administrador/PutAdministrador", content);
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
                await client.DeleteAsync($"api/Administrador/DeleteAdministrador/{id}");
            }
            return RedirectToAction("Index");
        }
    }
}