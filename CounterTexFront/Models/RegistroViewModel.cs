using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
    /// <summary>
    /// ViewModel para el registro de nuevos usuarios.
    /// </summary>
    public class RegistroViewModel
    {
        /// <summary>
        /// Nombres del usuario.
        /// </summary>
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombres { get; set; }

        /// <summary>
        /// Apellidos del usuario.
        /// </summary>
        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string Apellidos { get; set; }

        /// <summary>
        /// Documento de identidad del usuario.
        /// </summary>
        [Required(ErrorMessage = "El documento es obligatorio.")]
        public string Documento { get; set; }

        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo válido.")]
        public string Correo { get; set; }

        /// <summary>
        /// Contraseña del usuario.
        /// </summary>
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }

        /// <summary>
        /// Confirmación de la contraseña.
        /// </summary>
        [Required(ErrorMessage = "Debe confirmar la contraseña.")]
        [DataType(DataType.Password)]
        [Compare("Contraseña", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmarContraseña { get; set; }

        /// <summary>
        /// Rol del usuario (se envía a la API, no se muestra en la vista).
        /// </summary>
        public string Rol { get; set; }

        /// <summary>
        /// Edad del usuario (se envía a la API, no se muestra en la vista).
        /// </summary>
        public int Edad { get; set; }

        /// <summary>
        /// Teléfono del usuario (se envía a la API, no se muestra en la vista).
        /// </summary>
        public string Telefono { get; set; }
    }
}
