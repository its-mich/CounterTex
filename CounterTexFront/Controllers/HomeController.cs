using System;
using System.Configuration;
using System.Web.Mvc;

namespace CounterTexFront.Controllers
{
    public class HomeController : BaseController
    {
        string apiUrl = ConfigurationManager.AppSettings["Api"].ToString();

        public ActionResult Index()
        {

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

        public ActionResult Welcome()
        {
            return View();
        }
    }
}
