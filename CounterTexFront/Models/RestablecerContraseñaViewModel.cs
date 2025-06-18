using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class RestablecerContraseñaViewModel
    {
        public string Correo { get; set; }

        [Required]
        public string Codigo { get; set; }

        [Required]
        [MinLength(6)]
        public string NuevaContraseña { get; set; }

        [Required]
        [Compare("NuevaContraseña", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmarContraseña { get; set; }
    }
}