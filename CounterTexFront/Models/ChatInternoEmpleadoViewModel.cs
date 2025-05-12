using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class ChatInternoEmpleadoViewModel
	{
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio.")]
        [StringLength(50)]
        public string Rol { get; set; } // Ejemplo: "Administrador", "Empleado"

        [StringLength(250)]
        public string FotoUrl { get; set; } // Ruta a la imagen de perfil
    }
}