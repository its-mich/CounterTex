using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CounterTexFront.Models;
using Newtonsoft.Json;
using System.Web.Security;
using System.Net.Http;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;

namespace CounterTexFront.Controllers
{
    public class HomeController : Controller
    {
        string apiUrl = ConfigurationManager.AppSettings["Api"].ToString();

        public ActionResult Index()
        {
            return View();
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