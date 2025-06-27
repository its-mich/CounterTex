using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace CounterTexFront.Models
{
    /// <summary>
    /// Modelo de vista para representar una conversación de chat entre dos usuarios.
    /// </summary>
    public class ChatViewModel
    {
        /// <summary>
        /// ID del remitente (usuario que envía el mensaje).
        /// </summary>
        [Required]
        public int RemitenteId { get; set; }

        /// <summary>
        /// ID del destinatario (usuario que recibe el mensaje).
        /// </summary>
        [Required]
        public int DestinatarioId { get; set; }

        /// <summary>
        /// Nombre del remitente.
        /// </summary>
        [Display(Name = "Nombre del Remitente")]
        public string RemitenteNombre { get; set; }

        /// <summary>
        /// Nombre del destinatario.
        /// </summary>
        [Display(Name = "Nombre del Destinatario")]
        public string DestinatarioNombre { get; set; }

        /// <summary>
        /// Lista de mensajes intercambiados en el chat.
        /// </summary>
        [Display(Name = "Historial de Mensajes")]
        public List<MensajeChatDTO> Mensajes { get; set; }
    }

    /// <summary>
    /// DTO que representa un mensaje dentro de un chat.
    /// </summary>
    public class MensajeChatDTO
    {
        /// <summary>
        /// ID único del mensaje.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Contenido del mensaje.
        /// </summary>
        [Required(ErrorMessage = "El mensaje no puede estar vacío.")]
        [StringLength(500, ErrorMessage = "El mensaje no puede exceder los 500 caracteres.")]
        [JsonProperty("mensaje")]
        public string Mensaje { get; set; }

        /// <summary>
        /// Fecha y hora en que se envió el mensaje.
        /// </summary>
        [JsonProperty("fechaHora")]
        [Display(Name = "Fecha y Hora")]
        public DateTime FechaHora { get; set; }

        /// <summary>
        /// ID del usuario que envió el mensaje.
        /// </summary>
        [JsonProperty("remitenteId")]
        public int RemitenteId { get; set; }

        /// <summary>
        /// ID del usuario que recibió el mensaje.
        /// </summary>
        [JsonProperty("destinatarioId")]
        public int DestinatarioId { get; set; }
    }
}
