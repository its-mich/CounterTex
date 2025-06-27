using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
    /// <summary>
    /// Modelo de vista que representa un registro de horario para un empleado.
    /// </summary>
    public class HorarioViewModel
    {
        /// <summary>
        /// Identificador del empleado al que pertenece el horario.
        /// </summary>
        public int EmpleadoId { get; set; }

        /// <summary>
        /// Tipo de horario registrado (por ejemplo: Entrada, Salida).
        /// </summary>
        public string Tipo { get; set; }

        /// <summary>
        /// Hora específica del registro.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:hh\\:mm\\:ss}", ApplyFormatInEditMode = true)]
        public TimeSpan Hora { get; set; }

        /// <summary>
        /// Fecha correspondiente al registro de horario.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Observaciones adicionales relacionadas al registro de horario.
        /// </summary>
        public string Observaciones { get; set; }
    }
}
