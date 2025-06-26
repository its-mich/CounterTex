using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class ProduccionViewModel
	{
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal TotalValor { get; set; }
        public int UsuarioId { get; set; }
        public int PrendaId { get; set; }

        public string NombreUsuario { get; set; } 
        public string NombrePrenda { get; set; }  

        public List<ProduccionDetalleViewModel> ProduccionDetalles { get; set; }
    }

    public class ProduccionDetalleViewModel
    {
        public int Id { get; set; }
        public int ProduccionId { get; set; }
        public int Cantidad { get; set; }
        public int OperacionId { get; set; }
        public decimal ValorTotal { get; set; }

        public string NombreOperacion { get; set; }   
        public decimal? ValorOperacion { get; set; }   
    }
}