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
            var rol = System.Web.HttpContext.Current.Session["UserRole"]?.ToString();

            if (rol == "Administrador")
            {
                ViewBag.Layout = "~/Views/Shared/_LayoutAdmin.cshtml";
            }
            else if (rol == "Empleado")
            {
                ViewBag.Layout = "~/Views/Shared/_LayoutEmpleado.cshtml";
            }
            else if (rol == "Proveedor")
            {
                ViewBag.Layout = "~/Views/Shared/_LayoutProveedor.cshtml";
            }
            else
            {
                ViewBag.Layout = "~/Views/Shared/_Layout.cshtml";
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
