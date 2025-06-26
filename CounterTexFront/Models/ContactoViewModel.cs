using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
    public class ContactoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El asunto es obligatorio.")]
        public string Asunto { get; set; }

        [Required(ErrorMessage = "El mensaje es obligatorio.")]
        public string Mensaje { get; set; }

        public string Fecha { get; set; }
    }
}