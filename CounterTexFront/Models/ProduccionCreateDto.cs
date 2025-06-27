using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace CounterTexFront.Models
{
    /// <summary>
    /// DTO para crear una nueva producción con detalles asociados.
    /// </summary>
    public class ProduccionCreateDto
    {
        /// <summary>
        /// Fecha en la que se realiza la producción.
        /// </summary>
        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; }

        /// <summary>
        /// ID del usuario que está registrando la producción.
        /// </summary>
        [Required(ErrorMessage = "El ID del usuario es obligatorio.")]
        public int UsuarioId { get; set; }

        /// <summary>
        /// ID de la prenda asociada a la producción.
        /// </summary>
        [Required(ErrorMessage = "Debe seleccionar una prenda.")]
        public int PrendaId { get; set; }

        /// <summary>
        /// Lista de detalles de producción (cantidad por operación).
        /// </summary>
        [Required(ErrorMessage = "Debe agregar al menos un detalle de producción.")]
        public List<ProduccionDetalleCreateDto> ProduccionDetalles { get; set; }
    }

    /// <summary>
    /// DTO para detallar cada operación realizada en una producción.
    /// </summary>
    public class ProduccionDetalleCreateDto
    {
        /// <summary>
        /// Cantidad de veces que se realizó la operación.
        /// </summary>
        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero.")]
        public int Cantidad { get; set; }

        /// <summary>
        /// ID de la operación realizada.
        /// </summary>
        [Required(ErrorMessage = "Debe seleccionar una operación.")]
        public int OperacionId { get; set; }
    }
}
