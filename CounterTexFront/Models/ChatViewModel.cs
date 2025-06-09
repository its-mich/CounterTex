using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class ChatViewModel
	{
        public int RemitenteId { get; set; }
        public int DestinatarioId { get; set; }
        public string RemitenteNombre { get; set; }
        public string DestinatarioNombre { get; set; }
        public List<MensajeChatDTO> Mensajes { get; set; }
    }

    public class MensajeChatDTO
    {
        public int Id { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaHora { get; set; }
        public int RemitenteId { get; set; }
        public int DestinatarioId { get; set; }
    }
}
