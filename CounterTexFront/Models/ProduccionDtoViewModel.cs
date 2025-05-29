using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
    public class ProduccionDtoViewModel
    {
        public int Id { get; set; }
        public DateTime FechaProduccion { get; set; }
        public string Usuario { get; set; } // Nombre del usuario
        public string Prenda { get; set; } // Nombre de la prenda
        public string TipoPrenda { get; set; } // Si existe en tu DTO de la API
        public string Color { get; set; } // Si existe en tu DTO de la API
        public string Modelo { get; set; } // Si existe en tu DTO de la API
        public string Talla { get; set; } // Si existe en tu DTO de la API
        public string Operacion { get; set; } // Nombre de la operación
        public int Cantidad { get; set; }
        public string Estado { get; set; } // Si existe en tu DTO de la API
        public decimal? Total { get; set; } // Si el total puede ser null, hazlo decimal?
    }
}