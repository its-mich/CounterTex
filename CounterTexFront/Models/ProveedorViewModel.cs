using System;
using System.ComponentModel.DataAnnotations;

namespace CounterTexFront.Models
{
    /// <summary>
    /// ViewModel que representa la información de un proveedor.
    /// </summary>
    public class ProveedorViewModel
    {
        /// <summary>
        /// Identificador único del proveedor.
        /// </summary>
        [Key]
        public int IdProveedor { get; set; }

        /// <summary>
        /// Precio por unidad de prenda suministrada.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "El precio de la prenda debe ser un valor positivo.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Precio por Prenda")]
        public decimal PrecioPrenda { get; set; }

        /// <summary>
        /// Tipo de prenda que suministra el proveedor.
        /// </summary>
        [Required(ErrorMessage = "El tipo de prenda es obligatorio.")]
        [Display(Name = "Tipo de Prenda")]
        public string TipoPrenda { get; set; }

        /// <summary>
        /// Número de teléfono del proveedor.
        /// </summary>
        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "Ingrese un número de teléfono válido.")]
        [Display(Name = "Teléfono del Proveedor")]
        public string Telefono { get; set; }

        /// <summary>
        /// Nombre del proveedor.
        /// </summary>
        [Required(ErrorMessage = "El nombre del proveedor es obligatorio.")]
        [Display(Name = "Nombre del Proveedor")]
        public string NombreProveedor { get; set; }

        /// <summary>
        /// Dirección del proveedor.
        /// </summary>
        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        /// <summary>
        /// Ciudad en la que se encuentra el proveedor.
        /// </summary>
        [Required(ErrorMessage = "La ciudad es obligatoria.")]
        [Display(Name = "Ciudad")]
        public string Ciudad { get; set; }

        /// <summary>
        /// Localidad del proveedor (opcional).
        /// </summary>
        [Display(Name = "Localidad")]
        public string Localidad { get; set; }

        /// <summary>
        /// Barrio del proveedor (opcional).
        /// </summary>
        [Display(Name = "Barrio")]
        public string Barrio { get; set; }

        /// <summary>
        /// Cantidad de prendas suministradas por el proveedor.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad de prendas no puede ser negativa.")]
        [Display(Name = "Cantidad de Prendas Suministradas")]
        public int CantidadPrendas { get; set; }
    }
}
