using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
    public class ProduccionDiariaViewModel
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal TotalValor { get; set; }
        public int UsuarioId { get; set; }
        public int PrendaId { get; set; }
        public List<ProduccionDetalleViewModel> Detalles { get; set; }
    }
}