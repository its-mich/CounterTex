using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
    public class ProduccionDetalleViewModel
    {

        public DateTime Fecha { get; set; }
        public int UsuarioId { get; set; }
        public int PrendaId { get; set; }

        // Datos opcionales
        public string Color { get; set; }
        public string TipoPrenda { get; set; }
        public string Modelo { get; set; }
        public string Talla { get; set; }
        public string Efecto { get; set; }
        public string Acabado { get; set; }
        public string Observaciones { get; set; }

        // Lista de detalles
        public List<ProduccionDetalleViewModel> ProduccionDetalles { get; set; }
    }

}