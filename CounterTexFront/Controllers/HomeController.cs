using System;
using System.Configuration;
using System.Web.Mvc;

namespace CounterTexFront.Controllers
{
    /// <summary>
    /// Controlador principal del sitio. Gestiona las vistas generales como Index, About, Contact y Welcome.
    /// </summary>
    public class HomeController : BaseController
    {
        /// <summary>
        /// URL base de la API configurada en Web.config.
        /// </summary>
        string apiUrl = ConfigurationManager.AppSettings["Api"].ToString();

        /// <summary>
        /// Acción principal del Home. Muestra la vista de inicio para usuarios no autenticados.
        /// </summary>
        /// <returns>Vista Index.</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Muestra la página de descripción de la aplicación.
        /// </summary>
        /// <returns>Vista About.</returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        /// <summary>
        /// Muestra la página de contacto de la aplicación.
        /// </summary>
        /// <returns>Vista Contact.</returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        /// <summary>
        /// Muestra una vista pública de bienvenida, accesible sin autenticación.
        /// </summary>
        /// <returns>Vista Welcome.</returns>
        public ActionResult Welcome()
        {
            return View();
        }
    }
}
