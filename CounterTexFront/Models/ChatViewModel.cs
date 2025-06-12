using Newtonsoft.Json;
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
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("mensaje")]
        public string Mensaje { get; set; }

        [JsonProperty("fechaHora")]
        public DateTime FechaHora { get; set; }

        [JsonProperty("remitenteId")]
        public int RemitenteId { get; set; }

        [JsonProperty("destinatarioId")]
        public int DestinatarioId { get; set; }
    }
}
