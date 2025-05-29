using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class ProduccionDetalleViewModel
	{
        public int Id { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "La operación es obligatoria.")]
        [Display(Name = "Operación")]
        public int OperacionId { get; set; }

        // Propiedad de navegación para el detalle (si la API la devuelve)
        public OperacionViewModel Operacion { get; set; }

        // Si tu API calcula y devuelve un ValorTotal para cada detalle, inclúyelo aquí
        public decimal ValorTotal { get; set; }
    }
}