using System;
using System.ComponentModel.DataAnnotations;

namespace CounterTexFront.Models
{
    /// <summary>
    /// ViewModel que representa un pago realizado a un usuario/proveedor.
    /// </summary>
    public class PagoViewModel
    {
        /// <summary>
        /// Identificador único del pago.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID del usuario (proveedor o empleado) al que se le realiza el pago.
        /// </summary>
        [Required(ErrorMessage = "El usuario es obligatorio.")]
        [Display(Name = "Usuario")]
        public int UsuarioId { get; set; }

        /// <summary>
        /// Fecha de inicio del período que abarca el pago.
        /// </summary>
        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Inicio")]
        public DateTime FechaInicio { get; set; }

        /// <summary>
        /// Fecha de fin del período que abarca el pago.
        /// </summary>
        [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Fin")]
        public DateTime FechaFin { get; set; }

        /// <summary>
        /// Monto total pagado al usuario.
        /// </summary>
        [Required(ErrorMessage = "El total pagado es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El total pagado debe ser mayor a cero.")]
        [Display(Name = "Total Pagado")]
        public decimal TotalPagado { get; set; }

        /// <summary>
        /// Fecha en que se realizó el pago.
        /// </summary>
        [Required(ErrorMessage = "La fecha de pago es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Pago")]
        public DateTime FechaPago { get; set; }

        /// <summary>
        /// Observaciones o comentarios asociados al pago.
        /// </summary>
        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }

        /// <summary>
        /// Información del usuario relacionado con el pago.
        /// </summary>
        public UsuarioViewModel Usuario { get; set; }
    }
}
