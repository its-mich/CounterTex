using System;
using System.ComponentModel.DataAnnotations;

namespace CounterTexFront.Models
{
    public class MensajeChatViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Remitente")]
        public int RemitenteId { get; set; }

        public string RemitenteNombre { get; set; } // extra para mostrar el nombre

        [Display(Name = "Destinatario")]
        public int DestinatarioId { get; set; }

        public string DestinatarioNombre { get; set; } // extra para mostrar el nombre

        [Display(Name = "Fecha y Hora")]
        public DateTime FechaHora { get; set; }

        [Required(ErrorMessage = "El mensaje no puede estar vacío.")]
        [StringLength(1000, ErrorMessage = "El mensaje no debe exceder los 1000 caracteres.")]
        public string Mensaje { get; set; }
    }
}
