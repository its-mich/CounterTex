using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class Meta
	{
        
            [Required]
            public int UsuarioId { get; set; }

            [Required]
            [DataType(DataType.Date)]
            public DateTime Fecha { get; set; }

            [Required]
            [Display(Name = "Meta de Corte")]
            public int MetaCorte { get; set; }

            [Display(Name = "Producción Real")]
            public int ProduccionReal { get; set; }
    }
}