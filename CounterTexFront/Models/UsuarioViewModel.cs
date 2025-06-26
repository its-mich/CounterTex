using System.ComponentModel.DataAnnotations;

namespace CounterTexFront.Models
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre de Usuario")]
        public string Nombre { get; set; }

        public string Documento { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        public string Contraseña { get; set; }

        public int RolId { get; set; }

        public string RolNombre { get; set; }

        public int? Edad { get; set; }

        public string Telefono { get; set; }
    }
}
