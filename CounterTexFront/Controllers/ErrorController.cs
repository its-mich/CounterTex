using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CounterTexFront.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error

        [Route("Error/404")]
        public ActionResult NotFoundPage()
        {
            return View("404");
        }

        [Route("Error/500")]
        public ActionResult ServerError()
        {
            return View("500");
        }
    }
}