using CounterTexFront.Models;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using System.Collections.Generic;
using System.Web;

namespace CounterTexFront.Controllers
{
    public class AuthController : BaseController
    {
        private readonly string apiUrl = ConfigurationManager.AppSettings["Api"].ToString();

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Registro()
        {
            return View();
        }

        // REGISTRO DE USUARIO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registro(RegistroViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Contraseña != model.ConfirmarContraseña)
            {
                ModelState.AddModelError("", "Las contraseñas no coinciden.");
                return View(model);
            }

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Clear();

                    var usuario = new
                    {
                        id = 0,
                        nombre = $"{model.Nombres} {model.Apellidos}",
                        documento = model.Documento,
                        correo = model.Correo,
                        contraseña = model.Contraseña,
                        rolId = ObtenerRolId(model.Rol),
                        edad = model.Edad,
                        telefono = model.Telefono
                    };

                    var json = JsonConvert.SerializeObject(usuario);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("api/Usuarios/PostUsuarios", content);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["MensajeRegistro"] = "Usuario registrado correctamente. Ahora puedes iniciar sesión.";
                        return RedirectToAction("Login");
                    }

                    var error = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"Error al registrar: {error}");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error inesperado: " + ex.Message);
                return View(model);
            }
        }

        private int ObtenerRolId(string rol)
        {
            switch (rol)
            {
                case "Administrador":
                    return 1;
                case "Empleado":
                    return 2;
                case "Proveedor":
                    return 3;
                default:
                    return 0;
            }
        }

        // LOGIN
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Por favor verifica los campos del formulario.");
                return View(model);
            }

            // Validación del CAPTCHA
            string recaptchaResponse = Request["g-recaptcha-response"];
            string secretKey = ConfigurationManager.AppSettings["RecaptchaSecretKey"];

            using (var httpClient = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]
                {
            new KeyValuePair<string, string>("secret", secretKey),
            new KeyValuePair<string, string>("response", recaptchaResponse)
        });

                var captchaResult = await httpClient.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
                var captchaJson = await captchaResult.Content.ReadAsStringAsync();
                dynamic captchaVerify = JsonConvert.DeserializeObject(captchaJson);

                if (captchaVerify.success != true)
                {
                    ModelState.AddModelError("", "Por favor verifica que no eres un robot.");
                    return View(model);
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Clear();

                string json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/Auth/Login", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(result);

                    if (loginResponse?.Rol == null || string.IsNullOrEmpty(loginResponse.Rol))
                    {
                        ModelState.AddModelError("", "El rol del usuario no está asignado correctamente.");
                        return View(model);
                    }

                    // Guardar en sesión
                    Session["Usuario"] = loginResponse;
                    Session["BearerToken"] = loginResponse.Token;
                    Session["UserRole"] = loginResponse.Rol;
                    Session["NombreUsuario"] = loginResponse.Nombres;

                    // Crear el ticket de autenticación
                    var authTicket = new FormsAuthenticationTicket(
                        1,
                        loginResponse.Nombres, // Usuario
                        DateTime.Now,
                        DateTime.Now.AddMinutes(60), // duración
                        false,
                        loginResponse.Rol // puedes almacenar el rol
                    );

                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
                    {
                        HttpOnly = true
                    };
                    Response.Cookies.Add(authCookie);

                    // 🔥 Limpia la cookie antifalsificación para evitar conflictos de usuario
                    if (Request.Cookies["__RequestVerificationToken"] != null)
                    {
                        var tokenCookie = new HttpCookie("__RequestVerificationToken")
                        {
                            Expires = DateTime.Now.AddDays(-1),
                            HttpOnly = true
                        };
                        Response.Cookies.Add(tokenCookie);
                    }

                    // Redirección según rol
                    switch (loginResponse.Rol)
                    {
                        case "Administrador": return RedirectToAction("Index", "Administrador");
                        case "Proveedor": return RedirectToAction("Index", "Proveedor");
                        case "Empleado": return RedirectToAction("Index", "Empleado");
                        default: return RedirectToAction("Welcome", "Home");
                    }
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"Error del servidor: {errorContent}");
                    return View(model);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Welcome", "Home");
        }
    }
}
