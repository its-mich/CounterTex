using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class HorarioViewModel
	{
        public int EmpleadoId { get; set; }

        public string Tipo { get; set; }

        [DisplayFormat(DataFormatString = "{0:hh\\:mm\\:ss}", ApplyFormatInEditMode = true)]
        public TimeSpan Hora { get; set; }

        public DateTime Fecha { get; set; }

        public string Observaciones { get; set; }
    }
}