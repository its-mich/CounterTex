using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
    /// <summary>
    /// ViewModel para registrar un pago a un proveedor basado en la cantidad de prendas entregadas.
    /// </summary>
    public class PagoProveedorViewModel
    {
        /// <summary>
        /// Identificador de la prenda seleccionada.
        /// </summary>
        [Required]
        public int PrendaId { get; set; }

        /// <summary>
        /// Lista de prendas disponibles para selección en un dropdown.
        /// </summary>
        public IEnumerable<SelectListItem> PrendasDisponibles { get; set; }

        /// <summary>
        /// Cantidad de prendas entregadas por el proveedor.
        /// </summary>
        [Required(ErrorMessage = "Ingrese la cantidad de prendas.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0.")]
        public int CantidadPrendas { get; set; }

        /// <summary>
        /// Precio unitario acordado para cada prenda.
        /// </summary>
        [Required(ErrorMessage = "Ingrese el precio unitario.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0.")]
        public decimal PrecioUnitario { get; set; }

        /// <summary>
        /// Comentarios u observaciones adicionales del proveedor.
        /// </summary>
        public string Observaciones { get; set; }
    }
}
