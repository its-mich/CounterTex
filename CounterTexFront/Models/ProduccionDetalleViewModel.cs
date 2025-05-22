using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class ProduccionDetalleViewModel
	{
        public int OperacionId { get; set; }
        public int Cantidad { get; set; }
        public decimal? ValorTotal { get; set; }
    }
}