using CounterTexFront.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CounterTexFront.Models
{
    public class UsuarioViewModel
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [Display(Name = "Nombre de Usuario")]
        public string NombreUsuario { get; set; }

        public string Rol { get; set; }
        public int TokenValue { get; set; }
        public string Documento { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido.")]
        [Display(Name = "Correo Electrónico")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 100 caracteres.")]
        [Display(Name = "Contraseña")]
        public string Clave { get; set; }

        public int? OperacionId { get; set; } // Si OperacionId puede ser null en la API, también hazlo int?
        public int? Edad { get; set; } // ¡CORRECCIÓN CLAVE AQUÍ: int? para permitir null!
        public string Telefono { get; set; }

        public IEnumerable<UsuarioViewModel> UsuariosDisponibles { get; set; }
    }
}
