using System;
using System.ComponentModel.DataAnnotations;

namespace CounterTexFront.Models
{
    /// <summary>
    /// ViewModel que representa la información de las prendas entregadas por el proveedor.
    /// </summary>
    public class PrendasEntregadasViewModel
    {
        /// <summary>
        /// Identificador único del registro de prenda entregada.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre o tipo de la prenda entregada.
        /// </summary>
        [Required(ErrorMessage = "El nombre de la prenda es obligatorio.")]
        [Display(Name = "Nombre de la Prenda")]
        public string Nombre { get; set; }

        /// <summary>
        /// Género asociado a la prenda (masculino, femenino, unisex, etc.).
        /// </summary>
        [Required(ErrorMessage = "El género de la prenda es obligatorio.")]
        [Display(Name = "Género")]
        public string Genero { get; set; }

        /// <summary>
        /// Color de la prenda entregada.
        /// </summary>
        [Required(ErrorMessage = "El color de la prenda es obligatorio.")]
        [Display(Name = "Color")]
        public string Color { get; set; }

        /// <summary>
        /// Cantidad de prendas entregadas.
        /// </summary>
        [Required(ErrorMessage = "La cantidad de prendas es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad de prendas debe ser mayor que 0.")]
        [Display(Name = "Cantidad de Prendas")]
        public int CantidadPrendas { get; set; }
    }
}
