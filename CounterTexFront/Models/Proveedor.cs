using System;
using System.ComponentModel.DataAnnotations;

namespace CounterTexFront.Models
{
    public class Proveedor
    {
        [Key]
        public int IdProveedor { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El precio de la prenda debe ser un valor positivo.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Precio por Prenda")]
        public decimal PrecioPrenda { get; set; }

        [Required(ErrorMessage = "El tipo de prenda es obligatorio.")]
        [Display(Name = "Tipo de Prenda")]
        public string TipoPrenda { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "Ingrese un número de teléfono válido.")]
        [Display(Name = "Teléfono del Proveedor")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El nombre del proveedor es obligatorio.")]
        [Display(Name = "Nombre del Proveedor")]
        public string NombreProveedor { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "La ciudad es obligatoria.")]
        [Display(Name = "Ciudad")]
        public string Ciudad { get; set; }

        [Display(Name = "Localidad")]
        public string Localidad { get; set; }

        [Display(Name = "Barrio")]
        public string Barrio { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "La cantidad de prendas no puede ser negativa.")]
        [Display(Name = "Cantidad de Prendas Suministradas")]
        public int CantidadPrendas { get; set; }
    }

}
