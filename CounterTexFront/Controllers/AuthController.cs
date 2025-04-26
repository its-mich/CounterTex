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
                    ModelState.AddModelError("", "Error al iniciar sesión. Verifica tus credenciales.");
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
