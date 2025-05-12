using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class HorarioViewModel
	{
        [Required(ErrorMessage = "La fecha es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "La hora de entrada es obligatoria.")]
        [DataType(DataType.Time)]
        public TimeSpan HoraEntrada { get; set; }

        [Required(ErrorMessage = "La hora de salida es obligatoria.")]
        [DataType(DataType.Time)]
        public TimeSpan HoraSalida { get; set; }

        [Required(ErrorMessage = "El tiempo de descanso es obligatorio.")]
        [Range(0, 300, ErrorMessage = "El tiempo de descanso debe estar entre 0 y 300 minutos.")]
        public int HoraDescanso { get; set; }
    }
}