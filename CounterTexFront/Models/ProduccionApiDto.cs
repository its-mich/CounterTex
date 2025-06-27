using System;
using System.Collections.Generic;

namespace CounterTexFront.Models.DTOs
{
    public class ProduccionApiDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public PrendaDto Prenda { get; set; }
        public List<ProduccionDetalleDto> ProduccionDetalles { get; set; }

        public decimal? TotalValor { get; set; }

     
        public string PrendaNombre { get; set; }  // <-- Esto es lo que necesitabas


       

    }

    public class PrendaDto
    {
        public string Nombre { get; set; }

     
    }

    public class ProduccionDetalleDto
    {

      
        public int Cantidad { get; set; }
        public int OperacionId { get; set; }
    }
}
