using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CounterTexFront.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.RouteData.Values["controller"].ToString().ToLower();
            var action = filterContext.RouteData.Values["action"].ToString().ToLower();

            // Excluir acciones públicas
            bool esAccionPublica = controller == "auth" &&
                (action == "login" || action == "registro" || action == "recuperar");

            // ⚠️ Verificar que haya token
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

            // Layout según rol
            var rol = Session["UserRole"]?.ToString();
            if (rol == "Administrador")
                ViewBag.Layout = "~/Views/Shared/_LayoutAdmin.cshtml";
            else if (rol == "Empleado")
                ViewBag.Layout = "~/Views/Shared/_LayoutEmpleado.cshtml";
            else if (rol == "Proveedor")
                ViewBag.Layout = "~/Views/Shared/_LayoutProveedor.cshtml";

            ViewBag.NombreUsuario = Session["NombreUsuario"]?.ToString();

            base.OnActionExecuting(filterContext);
        }
    }
}
