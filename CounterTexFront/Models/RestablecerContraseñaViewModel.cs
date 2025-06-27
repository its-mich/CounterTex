using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
    /// <summary>
    /// ViewModel utilizado para restablecer la contraseña de un usuario.
    /// </summary>
    public class RestablecerContraseñaViewModel
    {
        /// <summary>
        /// Correo electrónico del usuario que solicita el restablecimiento.
        /// </summary>
        public string Correo { get; set; }

        /// <summary>
        /// Código de verificación enviado al usuario.
        /// </summary>
        [Required(ErrorMessage = "El código es obligatorio.")]
        public string Codigo { get; set; }

        /// <summary>
        /// Nueva contraseña que el usuario desea establecer.
        /// </summary>
        [Required(ErrorMessage = "La nueva contraseña es obligatoria.")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        [DataType(DataType.Password)]
        public string NuevaContraseña { get; set; }

        /// <summary>
        /// Confirmación de la nueva contraseña.
        /// </summary>
        [Required(ErrorMessage = "Debe confirmar la nueva contraseña.")]
        [Compare("NuevaContraseña", ErrorMessage = "Las contraseñas no coinciden.")]
        [DataType(DataType.Password)]
        public string ConfirmarContraseña { get; set; }
    }
}
