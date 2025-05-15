using Newtonsoft.Json;
using CounterTexFront.Models;
using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CounterTexFront.Controllers
{
    public class LoginController : BaseController
    {
        string apiUrl = ConfigurationManager.AppSettings["Api"].ToString();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
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
                    var token = JsonConvert.DeserializeObject<Tokens>(res);
                    CookieUpdate(model);
                    Session["BearerToken"] = token.TokenValue;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var errorContent = await Res.Content.ReadAsStringAsync();
                    ViewBag.ErrorLogin = $"No se pudo iniciar sesión. Detalle: {errorContent}";
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

        private void CookieUpdate(LoginViewModel usuario)
        {
            var ticket = new FormsAuthenticationTicket(2,
                usuario.UserName,
                DateTime.Now,
                DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),
                false,
                JsonConvert.SerializeObject(usuario)
            );

            Session["Username"] = usuario.UserName;
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket)) { };
            Response.AppendCookie(cookie);
        }
    }
}
