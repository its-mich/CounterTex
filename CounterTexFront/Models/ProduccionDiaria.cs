using System;
using System.ComponentModel.DataAnnotations;

public class ProduccionDiaria
{
    [Key] // Indica que esta propiedad es la clave primaria
    [Display(Name = "ID")]
    public string Id { get; set; } // Podría ser un string como en el ejemplo HTML

    [Required(ErrorMessage = "El Color es requerido.")]
    [Display(Name = "Color")]
    public string Color { get; set; }

    [Required(ErrorMessage = "El Tipo de Prenda es requerido.")]
    [Display(Name = "Tipo de Prenda")]
    public string TipoPrenda { get; set; }

    [Required(ErrorMessage = "El Modelo es requerido.")]
    [Display(Name = "Modelo")]
    public string Modelo { get; set; }

    [Required(ErrorMessage = "La Talla es requerida.")]
    [Display(Name = "Talla")]
    public string Talla { get; set; }

    [Required(ErrorMessage = "El Tipo de Operación es requerido.")]
    [Display(Name = "Operación")]
    public string Operacion { get; set; }

    [Required(ErrorMessage = "La Cantidad es requerida.")]
    [Range(1, int.MaxValue, ErrorMessage = "La Cantidad debe ser mayor que 0.")]
    [Display(Name = "Cantidad")]
    public int Cantidad { get; set; }

    [Display(Name = "Observaciones")]
    public string Observaciones { get; set; }

    [Display(Name = "Fecha")]
    [DataType(DataType.Date)]
    public DateTime Fecha { get; set; } = DateTime.Now; // Establecer la fecha actual por defecto

    [Display(Name = "Estado")]
    public string Estado { get; set; }

    // Podríamos tener propiedades de navegación si hay relaciones con otras tablas
}