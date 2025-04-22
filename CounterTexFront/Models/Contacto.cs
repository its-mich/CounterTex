using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class Contacto
	{
        [Required]
        [Display(Name = "Nombre Completo")]
        [StringLength(100)]
        public string NombreCompleto { get; set; }

        [Phone]
        [StringLength(20)]
        public string Telefono { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Correo { get; set; }

        [StringLength(500)]
        public string Observacion { get; set; }
    }
}