using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
    /// <summary>
    /// ViewModel que representa una operación realizada en el sistema, como una actividad de producción.
    /// </summary>
    public class OperacionViewModel
    {
        /// <summary>
        /// Identificador único de la operación.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la operación.
        /// </summary>
        [Required(ErrorMessage = "El nombre de la operación es obligatorio.")]
        [Display(Name = "Nombre de la Operación")]
        public string Nombre { get; set; }

        /// <summary>
        /// Valor monetario unitario asociado a la operación.
        /// </summary>
        [Display(Name = "Valor Unitario")]
        [Range(0, double.MaxValue, ErrorMessage = "El valor unitario debe ser mayor o igual a 0.")]
        public decimal? ValorUnitario { get; set; }
    }
}
