using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class ChatInterno
	{
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "El mensaje no puede estar vacío.")]
        [StringLength(500, ErrorMessage = "El mensaje no debe superar los 500 caracteres.")]
        public string Contenido { get; set; }

        [Required]
        public string Remitente { get; set; } // "usuario", "sistema", "admin", etc.

        public DateTime FechaEnvio { get; set; } = DateTime.Now;
    }
}