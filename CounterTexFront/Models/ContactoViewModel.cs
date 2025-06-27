using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
    /// <summary>
    /// Modelo de vista que representa un mensaje de contacto enviado por un usuario.
    /// </summary>
    public class ContactoViewModel
    {
        /// <summary>
        /// Identificador único del mensaje de contacto.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Asunto del mensaje enviado por el usuario.
        /// </summary>
        [Required(ErrorMessage = "El asunto es obligatorio.")]
        public string Asunto { get; set; }

        /// <summary>
        /// Contenido del mensaje enviado por el usuario.
        /// </summary>
        [Required(ErrorMessage = "El mensaje es obligatorio.")]
        public string Mensaje { get; set; }

        /// <summary>
        /// Fecha en que se envió el mensaje.
        /// </summary>
        public string Fecha { get; set; }
    }
}
