using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CounterTexFront.Models;
using CounterTexFront.Models.viewmodels;
using Newtonsoft.Json;
using RunGymFront.Models.ViewModels;

namespace CounterTexFront.Controllers
{
    public class CuentaController : Controller
    {
        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registro(Usuario model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://localhost:5001/api/usuarios", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Mensaje"] = "Registro exitoso.";
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "Error al registrar usuario.");
                    return View(model);
                }
            }
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Asegúrate que esta ruta exista en tu backend
                var response = await client.PostAsync("https://localhost:5001/api/usuarios/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var usuario = JsonConvert.DeserializeObject<UsuarioSesionViewModel>(jsonData);

                    // Guardar en sesión
                    Session["Usuario"] = usuario;
                    Session["Nombre"] = usuario.Nombres;
                    Session["Rol"] = usuario.Rol;

                    TempData["Mensaje"] = "Inicio de sesión exitoso.";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Credenciales incorrectas.");
                    return View(model);
                }
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();          // Borra toda la sesión
            Session.Abandon();        // Cierra la sesión completamente

            TempData["Mensaje"] = "Has cerrado sesión correctamente.";
            return RedirectToAction("Index", "Home");
        }




        public ActionResult Contacto()
        {
            return View();
        }
    }
}
