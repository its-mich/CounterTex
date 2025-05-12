using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class DetallesdePrendaViewModel
	{
        [Required(ErrorMessage = "El color es requerido.")]
        [Display(Name = "Color")]
        public string Color { get; set; }

        [Required(ErrorMessage = "El tipo de prenda es requerido.")]
        [Display(Name = "Tipo de Prenda")]
        public string TipoPrenda { get; set; }

        [Required(ErrorMessage = "El modelo es requerido.")]
        [Display(Name = "Modelo")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "El folio es requerido.")]
        [Display(Name = "Folio")]
        public string Folio { get; set; }

        [Required(ErrorMessage = "El tipo de operación es requerido.")]
        [Display(Name = "Tipo de Operación")]
        public string Operacion { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0.")]
        [Display(Name = "Cantidad")]
        public int Cantidad { get; set; }

        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }

        [Display(Name = "Talla")]
        public string Talla { get; set; }

        [Required(ErrorMessage = "La fecha es requerida.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }

        [Display(Name = "Efecto")]
        public string Efecto { get; set; }

        [Display(Name = "Acabado")]
        public string Acabado { get; set; }
    }
}