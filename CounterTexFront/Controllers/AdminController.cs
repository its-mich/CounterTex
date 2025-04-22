using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CounterTexFront.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult ControlPrendas() => View();
        public ActionResult Ganancias() => View();
        public ActionResult Gastos() => View();
        public ActionResult Pagos() => View();
    }
}