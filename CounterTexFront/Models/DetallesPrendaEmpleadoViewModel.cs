using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
    public class DetallesPrendaEmpleadoViewModel
    {

            [Required(ErrorMessage = "El color es obligatorio.")]
            [Display(Name = "Color")]
            public string Color { get; set; }

            [Required(ErrorMessage = "El tipo de prenda es obligatorio.")]
            [Display(Name = "Tipo de Prenda")]
            public string TipoPrenda { get; set; }

            [Required(ErrorMessage = "El modelo es obligatorio.")]
            [StringLength(100)]
            public string Modelo { get; set; }

            [Required(ErrorMessage = "El folio es obligatorio.")]
            [StringLength(50)]
            public string Folio { get; set; }

            [Required(ErrorMessage = "La operación es obligatoria.")]
            [Display(Name = "Tipo de Operación")]
            public string Operacion { get; set; }

            [Required(ErrorMessage = "La cantidad es obligatoria.")]
            [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero.")]
            public int Cantidad { get; set; }

            [StringLength(500)]
            public string Observaciones { get; set; }

            [Required(ErrorMessage = "La talla es obligatoria.")]
            public string Talla { get; set; }

            [Required(ErrorMessage = "La fecha es obligatoria.")]
            [DataType(DataType.Date)]
            public DateTime Fecha { get; set; }

            [Required(ErrorMessage = "El efecto es obligatorio.")]
            public string Efecto { get; set; }

            [Required(ErrorMessage = "El acabado es obligatorio.")]
            public string Acabado { get; set; }

        [Required]
        public int UsuarioId { get; set; }  // <- Nuevo campo

        [Required]
        public int PrendaId { get; set; }   // <- Nuevo campo

        [Required]
        public int OperacionId { get; set; }  // <- Nuevo campo


    }
}