
using System;
using System.ComponentModel.DataAnnotations;
using CounterTexFront.Models;

namespace CounterTexFront.Models
{
    public class SateliteViewModel//de la nasa
    {
        [Key]
        public int SateliteId { get; set; }

        [Required(ErrorMessage = "El fabricante es obligatorio.")]
        [Display(Name = "Fabricante")]
        public string Fabricante { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El pago por prendas debe ser un valor positivo.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Pago por Prendas")]
        public decimal PagoPrendas { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Las ganancias deben ser un valor positivo.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Ganancias")]
        public decimal Ganancias { get; set; }

        [Required(ErrorMessage = "La operación es obligatoria.")]
        [Display(Name = "Operación")]
        public string Operacion { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El pago por operación debe ser un valor positivo.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Pago por Operación")]
        public decimal PagoOperacion { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El inventario de máquinas no puede ser negativo.")]
        [Display(Name = "Inventario de Máquinas")]
        public int Inventariomaquinas { get; set; }

        [Required(ErrorMessage = "El tipo de máquina es obligatorio.")]
        [Display(Name = "Tipo de Máquina")]
        public string TipoMaquina { get; set; }

        [Required(ErrorMessage = "El ID de usuario es obligatorio.")]
        [Display(Name = "ID de Usuario")]
        public int IdUsuario { get; set; }
    }

}
