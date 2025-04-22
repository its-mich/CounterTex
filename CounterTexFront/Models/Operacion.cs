using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class Operacion
	{

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Valor Unitario")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El valor debe ser mayor que 0.")]
        public decimal ValorUnitario { get; set; }
    }
}