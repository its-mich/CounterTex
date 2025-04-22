using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models.viewmodels
{
	public class ProduccionDiariaViewModel
	{
        [Required]
            [DataType(DataType.Date)]
            [Display(Name = "Fecha")]
            public DateTime Fecha { get; set; }

            [Required]
            [Display(Name = "Total Valor")]
            public decimal TotalValor { get; set; }

            [Required]
            public int UsuarioId { get; set; }

            [Required]
            [Display(Name = "Prenda")]
            public int PrendaId { get; set; }
        
    }
}