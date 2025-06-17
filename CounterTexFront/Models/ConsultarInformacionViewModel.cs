using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class ConsultarInformacionViewModel
	{

        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre de la prenda")]
        public string Nombre { get; set; }

        [Display(Name = "Género")]
        public string Genero { get; set; }

        [Display(Name = "Color")]
        public string Color { get; set; }

    }
}