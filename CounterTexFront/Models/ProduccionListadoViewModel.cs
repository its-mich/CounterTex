using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class ProduccionListaViewModel
	{


        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int UsuarioId { get; set; }
        public string UsuarioNombre { get; set; } // Asegúrate que el backend lo envíe si lo necesitas
        public int PrendaId { get; set; }
        public string PrendaNombre { get; set; }  // Igual, el backend debe incluir esto si lo quieres mostrar
        public decimal TotalValor { get; set; }
    }
}