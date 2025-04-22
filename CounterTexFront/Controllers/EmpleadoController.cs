
using CounterTexFront.Models.viewmodels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace CounterTexFront.Controllers
{
    public class EmpleadoController : Controller
    {

        
            public ActionResult EditarPerfil() => View();
            public ActionResult ControlHorarios() => View();
            public ActionResult ConsultarInformacion() => View();
            public ActionResult MetaPorCorte() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MetaPorCorte(MetaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Recuperar el ID del usuario desde la sesión (ya logueado)
            if (Session["Usuario"] is UsuarioSesionViewModel usuario)
                model.UsuarioId = usuario.Id;
            else
                return RedirectToAction("Login", "Cuenta");

            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://localhost:5001/api/metas", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Mensaje"] = "Meta registrada correctamente.";
                    return RedirectToAction("MetaPorCorte");
                }
                else
                {
                    ModelState.AddModelError("", "Error al registrar la meta.");
                    return View(model);
                }
            }
        }

    }
}