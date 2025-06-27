using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CounterTexFront.Models
{
    /// <summary>
    /// ViewModel que representa una producción realizada por un usuario.
    /// </summary>
    public class ProduccionViewModel
    {
        /// <summary>
        /// Identificador único de la producción.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Fecha en que se realizó la producción.
        /// </summary>
        [Display(Name = "Fecha de Producción")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Valor total de la producción.
        /// </summary>
        [Display(Name = "Total de la Producción")]
        [Range(0, double.MaxValue, ErrorMessage = "El valor total debe ser un número positivo.")]
        public decimal TotalValor { get; set; }

        /// <summary>
        /// Identificador del usuario que realizó la producción.
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        /// Identificador de la prenda producida.
        /// </summary>
        public int PrendaId { get; set; }

        /// <summary>
        /// Nombre del usuario (solo para mostrar en la vista).
        /// </summary>
        public string NombreUsuario { get; set; }

        /// <summary>
        /// Nombre de la prenda (solo para mostrar en la vista).
        /// </summary>
        public string NombrePrenda { get; set; }

        /// <summary>
        /// Lista de operaciones realizadas en la producción.
        /// </summary>
        public List<ProduccionDetalleViewModel> ProduccionDetalles { get; set; }
    }

    /// <summary>
    /// ViewModel que representa el detalle de una operación dentro de una producción.
    /// </summary>
    public class ProduccionDetalleViewModel
    {
        /// <summary>
        /// Identificador del detalle.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID de la producción a la que pertenece este detalle.
        /// </summary>
        public int ProduccionId { get; set; }

        /// <summary>
        /// Cantidad de operaciones realizadas.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public int Cantidad { get; set; }

        /// <summary>
        /// ID de la operación realizada.
        /// </summary>
        public int OperacionId { get; set; }

        /// <summary>
        /// Valor total de la operación (cantidad * valor unitario).
        /// </summary>
        [Display(Name = "Valor Total")]
        [Range(0, double.MaxValue, ErrorMessage = "El valor total debe ser positivo.")]
        public decimal ValorTotal { get; set; }

        /// <summary>
        /// Nombre de la operación (para mostrar en la vista).
        /// </summary>
        public string NombreOperacion { get; set; }

        /// <summary>
        /// Valor unitario de la operación (puede ser nulo si no está definido).
        /// </summary>
        [Display(Name = "Valor Unitario")]
        [Range(0, double.MaxValue, ErrorMessage = "El valor unitario debe ser positivo.")]
        public decimal? ValorOperacion { get; set; }
    }
}
