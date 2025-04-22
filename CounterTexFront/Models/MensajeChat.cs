using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class MensajeChat
	{
        [Required]
        public int RemitenteId { get; set; }

        [Required]
        public int DestinatarioId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Mensaje { get; set; }

        public DateTime FechaHora { get; set; } = DateTime.Now;
    }
}