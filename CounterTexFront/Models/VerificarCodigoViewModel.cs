using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class VerificarCodigoViewModel
	{
        [Required(ErrorMessage = "El código es obligatorio.")]
        [Display(Name = "Código de Verificación")]
        public string Code { get; set; }
    }
}