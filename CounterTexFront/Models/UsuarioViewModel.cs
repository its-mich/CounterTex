using System.ComponentModel.DataAnnotations;

namespace CounterTexFront.Models
{
    /// <summary>
    /// ViewModel que representa los datos de un usuario en el sistema.
    /// </summary>
    public class UsuarioViewModel
    {
        /// <summary>
        /// Identificador único del usuario.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre completo del usuario.
        /// </summary>
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [Display(Name = "Nombre de Usuario")]
        public string Nombre { get; set; }

        /// <summary>
        /// Número de documento de identidad del usuario.
        /// </summary>
        public string Documento { get; set; }

        /// <summary>
        /// Dirección de correo electrónico del usuario.
        /// </summary>
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string Correo { get; set; }

        /// <summary>
        /// Contraseña del usuario (puede ser omitida en vistas).
        /// </summary>
        public string Contraseña { get; set; }

        /// <summary>
        /// Identificador del rol asignado al usuario.
        /// </summary>
        public int RolId { get; set; }

        /// <summary>
        /// Nombre del rol asignado al usuario.
        /// </summary>
        public string RolNombre { get; set; }

        /// <summary>
        /// Edad del usuario.
        /// </summary>
        [Range(0, 120, ErrorMessage = "La edad debe estar entre 0 y 120.")]
        public int? Edad { get; set; }

        /// <summary>
        /// Número de teléfono del usuario.
        /// </summary>
        [Phone(ErrorMessage = "El número de teléfono no es válido.")]
        public string Telefono { get; set; }
    }
}
