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
using System.Diagnostics;
using System.Collections.Generic;

namespace CounterTexFront.Controllers
{
    public class AuthController : BaseController
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

                    var json = JsonConvert.SerializeObject(new
                    {
                        model.Nombres,
                        model.Apellidos,
                        model.Documento,
                        model.Correo,
                        Contraseña = model.Contraseña,
                        Rol = model.Rol
                    });

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("api/Usuarios", content);


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
                ModelState.AddModelError("", "Error en el servidor: " + ex.Message);
                return View(model);
            }
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

            // Validación del CAPTCHA
            string recaptchaResponse = Request["g-recaptcha-response"];
            string secretKey = "6LeXMz0rAAAAAFPzq4MtASqq6wpFXiim4ovZ-vSJ";

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
                HttpResponseMessage Res = await client.PostAsync("api/Auth/Login", content);

                if (Res.IsSuccessStatusCode)
                {
                    var res = await Res.Content.ReadAsStringAsync();
                    Tokens token = JsonConvert.DeserializeObject<Tokens>(res);

                    // Depuración para verificar el rol y token
                    Debug.WriteLine("Token: " + token.TokenValue);  // Verifica que el token esté presente
                    Debug.WriteLine("Rol: " + token.Rol);  // Verifica que el rol esté presente

                    if (string.IsNullOrEmpty(token.Rol))
                    {
                        ModelState.AddModelError("", "El rol del usuario no está asignado correctamente.");
                        return View(model);
                    }

                    // Asignar el rol a la sesión
                    Session["BearerToken"] = token.TokenValue;
                    Session["UserRole"] = token.Rol;

                    // Verificación en el valor de rol antes de redirigir
                    Debug.WriteLine("Session UserRole: " + Session["UserRole"]); // Verifica el rol almacenado en la sesión

                    // Forzar la redirección en función del rol
                    if (token.Rol == "Administrador")
                    {
                        Debug.WriteLine("Redirigiendo a PerfilAdministrador...");
                        return RedirectToAction("Index", "Administrador");
                    }
                    else if (token.Rol == "Proveedor")
                    {
                        Debug.WriteLine("Redirigiendo a PerfilProveedor...");
                        return RedirectToAction("Index", "Proveedor");
                    }
                    else if (token.Rol == "Empleado")
                    {
                        Debug.WriteLine("Redirigiendo a PerfilEmpleado...");
                        return RedirectToAction("Index", "Empleado");
                    }
                    else
                    {
                        // Si el rol no es ninguno de los anteriores, redirige a Home
                        Debug.WriteLine("Redirigiendo a Home por rol desconocido...");
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Credenciales incorrectas. Nombre de usuario o contraseña no válidos");
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
