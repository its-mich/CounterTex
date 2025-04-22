using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models.viewmodels
{
	public class UsuarioSesionViewModel
	{
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombres")]
        [StringLength(100)]
        public string Nombres { get; set; }

        [Required]
        [Display(Name = "Correo electrónico")]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        [Display(Name = "Rol del usuario")]
        [StringLength(20)]
        public string Rol { get; set; }
    }
}