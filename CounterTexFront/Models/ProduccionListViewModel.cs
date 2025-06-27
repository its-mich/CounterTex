using System;
using System.Collections.Generic;

namespace CounterTexFront.Models
{
    /// <summary>
    /// ViewModel que representa un listado paginado de producciones.
    /// </summary>
    public class ProduccionListViewModel
    {
        /// <summary>
        /// Lista de producciones a mostrar en la vista actual.
        /// </summary>
        public List<ProduccionDtoViewModel> ListaProducciones { get; set; }

        /// <summary>
        /// Número de la página actual.
        /// </summary>
        public int PaginaActual { get; set; }

        /// <summary>
        /// Número total de páginas disponibles.
        /// </summary>
        public int TotalPaginas { get; set; }

        /// <summary>
        /// Cantidad de elementos que se muestran por página.
        /// </summary>
        public int TamanoPagina { get; set; }

        /// <summary>
        /// Número total de registros en la base de datos.
        /// </summary>
        public int TotalRegistros { get; set; }
    }
}
