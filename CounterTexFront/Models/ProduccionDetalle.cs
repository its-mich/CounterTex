using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class ProduccionDetalle
	{
        [Required]
        [Display(Name = "Cantidad")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ingresar una cantidad válida.")]
        public int Cantidad { get; set; }

        [Required]
        [Display(Name = "Operación")]
        public int OperacionId { get; set; }

        [Required]
        [Display(Name = "Producción")]
        public int ProduccionId { get; set; }
    }
}