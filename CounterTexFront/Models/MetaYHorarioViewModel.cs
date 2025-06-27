using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
    /// <summary>
    /// ViewModel que agrupa una lista de metas de producción junto con los horarios registrados.
    /// </summary>
    public class MetaYHorarioViewModel
    {
        /// <summary>
        /// Lista de metas de producción registradas.
        /// </summary>
        public List<MetaViewModel> Metas { get; set; }

        /// <summary>
        /// Lista de horarios de entrada y salida del empleado.
        /// </summary>
        public List<HorarioViewModel> Horarios { get; set; }
    }
}
