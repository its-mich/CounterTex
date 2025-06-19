using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class OperacionViewModel
	{
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la operación es obligatorio.")]
        [Display(Name = "Nombre de la Operación")]
        public string Nombre { get; set; }

        [Display(Name = "Valor Unitario")]
        [Range(0, double.MaxValue, ErrorMessage = "El valor unitario debe ser mayor o igual a 0.")]
        public decimal? ValorUnitario { get; set; }
    }
}