using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
    /// <summary>
    /// ViewModel utilizado para solicitar la recuperación de contraseña a través del correo electrónico.
    /// </summary>
    public class SolicitudRecuperacionViewModel
    {
        /// <summary>
        /// Correo electrónico del usuario que solicita la recuperación de contraseña.
        /// </summary>
        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Correo inválido.")]
        public string Correo { get; set; }
    }
}
