using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models.viewmodels
{
	public class HorarioViewModel
	{
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }

        [Required]
        [Display(Name = "Hora de Entrada")]
        [DataType(DataType.Time)]
        public TimeSpan HoraEntrada { get; set; }

        [Required]
        [Display(Name = "Hora de Salida")]
        [DataType(DataType.Time)]
        public TimeSpan HoraSalida { get; set; }

        public int UsuarioId { get; set; } // Se asignará desde Session
    }
}