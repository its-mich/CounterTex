using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class Horario
	{
        [Required]
        public int UsuarioId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required]
        [Display(Name = "Hora de Entrada")]
        public TimeSpan HoraEntrada { get; set; }

        [Required]
        [Display(Name = "Hora de Salida")]
        public TimeSpan HoraSalida { get; set; }
    }
}