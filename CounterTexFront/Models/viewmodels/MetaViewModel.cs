using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models.viewmodels
{
	public class MetaViewModel
	{
        [Required]
        public int UsuarioId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Meta")]
        public DateTime Fecha { get; set; }

        [Required]
        [Display(Name = "Meta de Corte")]
        public int MetaCorte { get; set; }

        [Display(Name = "Producción Real")]
        public int ProduccionReal { get; set; }
    }
}