using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class MetaViewModel
	{
        public int Id { get; set; }

        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Display(Name = "Meta Corte")]
        public int MetaCorte { get; set; }

        [Display(Name = "Producción Real")]
        public int ProduccionReal { get; set; }

        [Display(Name = "Fecha y Hora")]
        [DataType(DataType.DateTime)]
        public DateTime FechaHora { get; set; }

        public string Mensaje { get; set; }

        public int UsuarioId { get; set; }
        public int RemitenteId { get; set; }
        public int DestinatarioId { get; set; }

    }
}