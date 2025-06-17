using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class ProduccionDetalleEmpleadoViewModel
	{
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public decimal? ValorTotal { get; set; }

        public int ProduccionId { get; set; }
        public string NombrePrenda { get; set; } // de Produccion.Prenda.Nombre
        public string ColorPrenda { get; set; }  // de Produccion.Prenda.Color

        public int OperacionId { get; set; }
        public string NombreOperacion { get; set; }

        public decimal ValorUnitario { get; set; }
    }
}