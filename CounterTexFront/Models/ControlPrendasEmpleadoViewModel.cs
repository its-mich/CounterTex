using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class ControlPrendasEmpleadoViewModel
	{
        public int Id { get; set; }

        [Required(ErrorMessage = "El tipo de prenda es obligatorio.")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "El modelo es obligatorio.")]
        [StringLength(50, ErrorMessage = "El modelo no puede exceder los 50 caracteres.")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "El color es obligatorio.")]
        [StringLength(30, ErrorMessage = "El color no puede exceder los 30 caracteres.")]
        public string Color { get; set; }

        [Required(ErrorMessage = "La talla es obligatoria.")]
        public string Talla { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "La fecha de producción es obligatoria.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaProduccion { get; set; }

        [StringLength(200, ErrorMessage = "Las notas no pueden exceder los 200 caracteres.")]
        public string Notas { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public string Estado { get; set; } // Ej: Disponible, En Producción, Completada, Rechazada
    }
}

