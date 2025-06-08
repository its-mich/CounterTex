using System.ComponentModel.DataAnnotations;

namespace CounterTexFront.Models
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [Display(Name = "Nombre de Usuario")]
        public string Nombre { get; set; }

        public string Documento { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido.")]
        [Display(Name = "Correo Electrónico")]
        public string Correo { get; set; }

        // Este campo solo lo necesitas si vas a crear/editar usuarios desde formulario
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 100 caracteres.")]
        [Display(Name = "Contraseña")]
        public string Contraseña { get; set; }

        public int RolId { get; set; }

        public string RolNombre { get; set; } = "Sin rol"; // <- vendrá de Usuario.Rol.Nombre en la API

        public int? Edad { get; set; }

        public string Telefono { get; set; }
    }
}
