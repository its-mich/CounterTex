using System;

namespace CounterTexFront.Models
{
    public class PagoViewModel
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal TotalPagado { get; set; }
        public DateTime FechaPago { get; set; }
        public string Observaciones { get; set; }

        public UsuarioViewModel Usuario { get; set; } 
    }
}
