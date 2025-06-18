using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class HorarioViewModel
	{
        public int HorarioId { get; set; }
        public int EmpleadoId { get; set; }

        public string Tipo { get; set; }

        public string Hora { get; set; }

        public DateTime Fecha { get; set; }

        public string Observaciones { get; set; }
    }
}