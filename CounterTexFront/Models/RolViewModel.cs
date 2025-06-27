using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
    /// <summary>
    /// ViewModel que representa un rol asignado a un usuario dentro del sistema.
    /// </summary>
    public class RolViewModel
    {
        /// <summary>
        /// Identificador único del rol.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre descriptivo del rol (Ej: Administrador, Empleado, Proveedor).
        /// </summary>
        public string Nombre { get; set; }
    }
}
