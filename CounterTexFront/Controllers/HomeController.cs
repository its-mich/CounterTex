using System;
using System.Configuration;
using System.Web.Mvc;

namespace CounterTexFront.Controllers
{
    public class HomeController : Controller
    {
        string apiUrl = ConfigurationManager.AppSettings["Api"].ToString();

        public ActionResult Index()
        {
            var rol = Session["UserRole"] as string;

            if (!string.IsNullOrEmpty(rol))
            {
                switch (rol)
                {
                    case "Administrador":
                        return RedirectToAction("Index", "PerfilAdministrador");

                    case "Proveedor":
                        return RedirectToAction("Index", "PerfilProveedor");

                    case "Empleado":
                        return RedirectToAction("Index", "PerfilEmpleado");

                    default:
                        break; // Puedes redirigir a un error o mostrar vista genérica
                }
            }

            return View(); // Vista por defecto para usuarios no autenticados
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult LoginPartial()
        {
            return PartialView("_LoginPartial");
        }

        public ActionResult Welcome()
        {
            return View();
        }
    }
}
