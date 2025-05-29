using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
    public class ProduccionListViewModel
    {
        // La lista de producciones que se mostrarán en la página actual
        public List<ProduccionDtoViewModel> ListaProducciones { get; set; }

        // Propiedades para la paginación
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
        public int TamanoPagina { get; set; } // Opcional, para saber cuántos elementos por página
        public int TotalRegistros { get; set; } // Opcional, el total de elementos en la base de datos
    }
}