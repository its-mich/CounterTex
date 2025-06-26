using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
    public class PagoProveedorViewModel
    {
        [Required]
        public int PrendaId { get; set; }

        public IEnumerable<SelectListItem> PrendasDisponibles { get; set; }

        [Required(ErrorMessage = "Ingrese la cantidad de prendas.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0.")]
        public int CantidadPrendas { get; set; }

        [Required(ErrorMessage = "Ingrese el precio unitario.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0.")]
        public decimal PrecioUnitario { get; set; }

        public string Observaciones { get; set; }
    }
}