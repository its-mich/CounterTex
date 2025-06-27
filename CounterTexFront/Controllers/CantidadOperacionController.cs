using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CounterTexFront.Controllers
{
    /// <summary>
    /// Controlador responsable de gestionar la vista de cantidad de operaciones realizadas.
    /// Hereda de <see cref="BaseController"/> para utilizar configuración de sesión, autenticación y layout dinámico.
    /// </summary>
    public class CantidadOperacionController : BaseController
    {
        /// <summary>
        /// Acción principal que devuelve la vista de índice para la funcionalidad de cantidad de operación.
        /// Esta vista puede ser utilizada para mostrar un resumen de operaciones realizadas por usuario o tipo.
        /// </summary>
        /// <returns>Vista <c>Index</c> correspondiente a <c>CantidadOperacion</c></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}
