using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
    /// <summary>
    /// Modelo de vista para la edición del perfil de un usuario.
    /// </summary>
    public class EditarPerfilViewModel
    {
        /// <summary>
        /// Identificador único del usuario.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombres del usuario.
        /// </summary>
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombres { get; set; }

        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        public string Correo { get; set; }

        /// <summary>
        /// Teléfono de contacto del usuario.
        /// </summary>
        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        public string Telefono { get; set; }

        /// <summary>
        /// Edad del usuario. Debe estar entre 18 y 100 años.
        /// </summary>
        [Required(ErrorMessage = "La edad es obligatoria.")]
        [Range(18, 100, ErrorMessage = "Edad debe estar entre 18 y 100.")]
        public int Edad { get; set; }

        /// <summary>
        /// Documento de identidad del usuario (solo lectura).
        /// </summary>
        public string Documento { get; set; }
    }
}
