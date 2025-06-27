using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
    /// <summary>
    /// Representa la meta de producción de un usuario, incluyendo fecha, producción real y destinatarios involucrados.
    /// </summary>
    public class MetaViewModel
    {
        /// <summary>
        /// Identificador único de la meta.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Fecha objetivo para la meta.
        /// </summary>
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Valor de la meta esperada en corte.
        /// </summary>
        [Display(Name = "Meta Corte")]
        public int MetaCorte { get; set; }

        /// <summary>
        /// Producción real alcanzada.
        /// </summary>
        [Display(Name = "Producción Real")]
        public int ProduccionReal { get; set; }

        /// <summary>
        /// Fecha y hora de la creación o actualización de la meta.
        /// </summary>
        [Display(Name = "Fecha y Hora")]
        [DataType(DataType.DateTime)]
        public DateTime FechaHora { get; set; }

        /// <summary>
        /// Mensaje asociado a la meta (puede contener observaciones).
        /// </summary>
        public string Mensaje { get; set; }

        /// <summary>
        /// ID del usuario que registra la meta.
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        /// ID del remitente en caso de interacción.
        /// </summary>
        public int RemitenteId { get; set; }

        /// <summary>
        /// ID del destinatario de la meta o notificación.
        /// </summary>
        public int DestinatarioId { get; set; }
    }
}
