using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CounterTexFront.Models.DTOs
{
    /// <summary>
    /// DTO que representa una producción registrada para una prenda específica, incluyendo sus detalles.
    /// </summary>
    public class ProduccionApiDto
    {
        /// <summary>
        /// Identificador único de la producción.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Fecha en la que se registró la producción.
        /// </summary>
        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Información de la prenda asociada a la producción.
        /// </summary>
        [Required(ErrorMessage = "Debe especificar una prenda.")]
        public PrendaDto Prenda { get; set; }

        /// <summary>
        /// Lista de operaciones realizadas durante la producción.
        /// </summary>
        [Required(ErrorMessage = "Debe incluir al menos un detalle de producción.")]
        public List<ProduccionDetalleDto> ProduccionDetalles { get; set; }

        /// <summary>
        /// Valor total de la producción calculado a partir de los detalles.
        /// </summary>
        public decimal? TotalValor { get; set; }
    }

    /// <summary>
    /// DTO que contiene el nombre de la prenda.
    /// </summary>
    public class PrendaDto
    {
        /// <summary>
        /// Nombre de la prenda.
        /// </summary>
        [Required(ErrorMessage = "El nombre de la prenda es obligatorio.")]
        public string Nombre { get; set; }
    }

    /// <summary>
    /// DTO que representa el detalle de una operación dentro de una producción.
    /// </summary>
    public class ProduccionDetalleDto
    {
        /// <summary>
        /// Cantidad de veces que se realizó la operación.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0.")]
        public int Cantidad { get; set; }

        /// <summary>
        /// Identificador de la operación realizada.
        /// </summary>
        [Required(ErrorMessage = "Debe seleccionar una operación.")]
        public int OperacionId { get; set; }
    }
}
