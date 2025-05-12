using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CounterTexFront.Controllers
{
    public class ControlHorariosController : Controller
    {
        // GET: ControlHorarios
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ControlHorarios()
        {
            return View();
        }



        //[HttpPost]
        //public ActionResult Create(CounterTexFront.Models.HorarioViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Aquí podrías guardar los datos si tuvieras base de datos
        //        TempData["Mensaje"] = "Horario guardado correctamente.";
        //        return RedirectToAction("Index"); // Redirige al listado (u otra vista)
        //    }

        //    return View(model); // Si hay errores, vuelve al formulario
        //}

    }
}