using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class ControlPagosViewModel
	{
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del empleado es obligatorio")]
        [Display(Name = "Empleado")]
        public string Empleado { get; set; }

        [Required(ErrorMessage = "La fecha de pago es obligatoria")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Pago")]
        public DateTime FechaPago { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio")]
        [DataType(DataType.Currency)]
        [Display(Name = "Monto Pagado")]
        public decimal Monto { get; set; }

        [Display(Name = "Forma de Pago")]
        public string MetodoPago { get; set; }

        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }
    }
}