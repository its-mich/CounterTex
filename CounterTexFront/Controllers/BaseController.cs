using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CounterTexFront.Controllers
{
    /// <summary>
    /// Controlador base que proporciona funcionalidad común para todos los controladores, 
    /// incluyendo verificación de sesión, selección de layout y utilidades de consumo de API.
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// Método que se ejecuta antes de cada acción. 
        /// Verifica si el usuario tiene sesión activa y aplica el layout según el rol.
        /// </summary>
        /// <param name="filterContext">Contexto de la acción actual</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.RouteData.Values["controller"].ToString().ToLower();
            var action = filterContext.RouteData.Values["action"].ToString().ToLower();

            // Acciones que no requieren autenticación
            bool esAccionPublica =
                (controller == "auth" &&
                    (action == "login" || action == "registro" || action == "recuperar" || action == "confirmarcodigo")) ||
                (controller == "home" && action == "welcome");

            // ⚠️ Redirigir al login si no hay token y la acción no es pública
            if (!esAccionPublica && Session["Bearertoken"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(new
                    {
                        controller = "Auth",
                        action = "Login"
                    })
                );
                return;
            }

            // Selección del layout según el rol del usuario
            var rol = Session["UserRole"]?.ToString();
            if (rol == "Administrador")
                ViewBag.Layout = "~/Views/Shared/_LayoutAdmin.cshtml";
            else if (rol == "Empleado")
                ViewBag.Layout = "~/Views/Shared/_LayoutEmpleado.cshtml";
            else if (rol == "Proveedor")
                ViewBag.Layout = "~/Views/Shared/_LayoutProveedor.cshtml";

            // Pasar el nombre del usuario a la vista
            ViewBag.NombreUsuario = Session["NombreUsuario"]?.ToString();

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// Realiza una solicitud GET a una API externa y deserializa la respuesta en un tipo genérico.
        /// </summary>
        /// <typeparam name="T">Tipo de dato esperado como respuesta</typeparam>
        /// <param name="client">Instancia de HttpClient ya configurada</param>
        /// <param name="endpoint">Ruta relativa o absoluta de la API</param>
        /// <returns>Instancia del tipo T si la respuesta es exitosa; de lo contrario, default(T)</returns>
        protected async Task<T> GetFromApi<T>(HttpClient client, string endpoint)
        {
            HttpResponseMessage response = await client.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }

            return default(T);
        }
    }
}
