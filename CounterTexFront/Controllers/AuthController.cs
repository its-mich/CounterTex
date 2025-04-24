using CounterTexFront.Models;
using Newtonsoft.Json;
using System;
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
    public class AuthController : Controller
    {
        string apiUrl = ConfigurationManager.AppSettings["Api"].ToString();

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Por favor verifica los campos del formulario.");
                return View(model);
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Clear();

                string json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("api/Auth/Login", content);

                if (Res.IsSuccessStatusCode)
                {
                    var res = await Res.Content.ReadAsStringAsync();
                    Tokens token = JsonConvert.DeserializeObject<Tokens>(res);
                    Session["BearerToken"] = token.TokenValue;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var errorResponse = await Res.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", "Error al iniciar sesión: " + errorResponse);
                    return View(model);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registro(RegistroViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Por favor verifica los campos del formulario.");
                return View(model);
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Clear();

                string json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("api/Auth/Registro", content);

                if (Res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login", "Auth");
                }
                else
                {
                    var errorResponse = await Res.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", "Error al registrar el usuario: " + errorResponse);
                    return View(model);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
