using CounterTexFront.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CounterTexFront.Controllers
{
    public class ControlPagosController : Controller
    {


  
        
            public ActionResult Index()
            {
                return View();
            }
        

        //// GET: ControlPagos
        //public ActionResult Index()
        //{
        //    var model = new ControlPagosViewModel();
        //    return View(model);
        //}

        //// POST: ControlPagos
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Index(ControlPagosViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Aquí puedes guardar los datos en la base de datos o hacer la lógica que necesites
        //        // Por ejemplo:
        //        // _controlPagosService.GuardarPago(model);

        //        // Luego puedes redirigir o mostrar un mensaje de éxito
        //        ViewBag.Message = "Pago guardado exitosamente.";
        //        return View(model);
        //    }

        //    // Si hay errores en el modelo, vuelve a mostrar el formulario
        //    return View(model);
        //}


        //public ActionResult ControlPagos()
        //{
        //    var model = new ControlPagosViewModel();
        //    return View(model);
        //}
    }
}