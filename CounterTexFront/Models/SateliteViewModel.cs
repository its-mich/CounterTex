using System;
using System.ComponentModel.DataAnnotations;

namespace CounterTexFront.Models
{
    /// <summary>
    /// ViewModel que representa información relacionada a un satélite de la NASA con datos económicos y técnicos.
    /// </summary>
    public class SateliteViewModel
    {
        /// <summary>
        /// Identificador único del satélite.
        /// </summary>
        [Key]
        public int SateliteId { get; set; }

        /// <summary>
        /// Nombre del fabricante del satélite.
        /// </summary>
        [Required(ErrorMessage = "El fabricante es obligatorio.")]
        [Display(Name = "Fabricante")]
        public string Fabricante { get; set; }

        /// <summary>
        /// Valor monetario pagado por prendas relacionadas con el satélite.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "El pago por prendas debe ser un valor positivo.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Pago por Prendas")]
        public decimal PagoPrendas { get; set; }

        /// <summary>
        /// Ganancias generadas por el satélite.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Las ganancias deben ser un valor positivo.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Ganancias")]
        public decimal Ganancias { get; set; }

        /// <summary>
        /// Tipo de operación que realiza el satélite.
        /// </summary>
        [Required(ErrorMessage = "La operación es obligatoria.")]
        [Display(Name = "Operación")]
        public string Operacion { get; set; }

        /// <summary>
        /// Pago correspondiente a la operación realizada.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "El pago por operación debe ser un valor positivo.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Pago por Operación")]
        public decimal PagoOperacion { get; set; }

        /// <summary>
        /// Número total de máquinas disponibles para la operación satelital.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "El inventario de máquinas no puede ser negativo.")]
        [Display(Name = "Inventario de Máquinas")]
        public int Inventariomaquinas { get; set; }

        /// <summary>
        /// Tipo de máquina utilizada por el satélite.
        /// </summary>
        [Required(ErrorMessage = "El tipo de máquina es obligatorio.")]
        [Display(Name = "Tipo de Máquina")]
        public string TipoMaquina { get; set; }

        /// <summary>
        /// Identificador del usuario responsable o asociado al satélite.
        /// </summary>
        [Required(ErrorMessage = "El ID de usuario es obligatorio.")]
        [Display(Name = "ID de Usuario")]
        public int IdUsuario { get; set; }
    }
}
