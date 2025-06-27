using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CounterTexFront.Controllers
{
    /// <summary>
    /// Controlador responsable de manejar la vista principal de control de pagos.
    /// Hereda de <see cref="BaseController"/> para aplicar configuración global como sesión, layout y autenticación.
    /// </summary>
    public class ControlPagosController : BaseController
    {
        /// <summary>
        /// Acción que devuelve la vista inicial del módulo de control de pagos.
        /// </summary>
        /// <returns>Vista Index para Control de Pagos.</returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}
