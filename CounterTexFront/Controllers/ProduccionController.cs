using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using CounterTexFront.Models;
using CounterTexFront.Models.viewmodels;

namespace CounterTexFront.Controllers
{
    public class ProduccionController : Controller
    {
        public ActionResult CantidadOperacion()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CantidadOperacion(ProduccionDetalle model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (var http = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await http.PostAsync("https://localhost:5001/api/produccion/detalle", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Mensaje"] = "Operación registrada correctamente";
                    return RedirectToAction("CantidadOperacion");
                }
                else
                {
                    ModelState.AddModelError("", "Error al registrar la operación.");
                    return View(model);
                }
            }
        }



        public ActionResult ProduccionDiaria()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]


        public async Task<ActionResult> ProduccionDiaria(ProduccionDiariaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (Session["Usuario"] is UsuarioSesionViewModel usuario)
                model.UsuarioId = usuario.Id;
            else
                return RedirectToAction("Login", "Cuenta");

            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://localhost:5001/api/produccion", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Mensaje"] = "Producción registrada correctamente.";
                    return RedirectToAction("ProduccionDiaria");
                }
                else
                {
                    ModelState.AddModelError("", "Error al registrar la producción.");
                    return View(model);
                }
            }

        }
    }
}
