using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class EditarPerfilViewModel
	{

        public int Id { get; set; }

        [Required]
        public string Nombres { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        [Range(18, 100, ErrorMessage = "Edad debe estar entre 18 y 100.")]
        public int Edad { get; set; }

        public string Documento { get; set; }

    }
}