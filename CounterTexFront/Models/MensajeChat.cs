using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class MensajeChat
	{
        [Required(ErrorMessage = "El texto del mensaje es obligatorio.")]
        [StringLength(200, ErrorMessage = "El mensaje no puede exceder los 200 caracteres.")]
        public string Texto { get; set; }

        public string Remitente { get; set; } // "usuario" o "sistema"

        public DateTime FechaHoraEnvio { get; set; } = DateTime.Now; // Valor predeterminado


    }
}