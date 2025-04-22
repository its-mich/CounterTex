using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models.viewmodels
{
	public class PrendaViewModel
	{
        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Género")]
        [MaxLength(20)]
        public string Genero { get; set; }

        [Display(Name = "Color")]
        [MaxLength(50)]
        public string Color { get; set; }
    }
}