using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class ProduccionDiariaEmpleadoViewModel
	{
        [Required(ErrorMessage = "El tipo de prenda es obligatorio.")]
        [Display(Name = "Tipo de Prenda")]
        public string TipoPrenda { get; set; }

        [Required(ErrorMessage = "El color es obligatorio.")]
        public string Color { get; set; }

        [Required(ErrorMessage = "El modelo es obligatorio.")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "La talla es obligatoria.")]
        public string Talla { get; set; }

        [Required(ErrorMessage = "La operación es obligatoria.")]
        [Display(Name = "Tipo de Operación")]
        public string Operacion { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ser mayor que cero.")]
        public int Cantidad { get; set; }

        [StringLength(500)]
        public string Observaciones { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}