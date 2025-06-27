using System;

namespace CounterTexFront.Models
{
    /// <summary>
    /// Modelo de vista que representa los datos de una producción obtenida desde la API.
    /// </summary>
    public class ProduccionDtoViewModel
    {
        /// <summary>
        /// Identificador único de la producción.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Fecha en que se realizó la producción.
        /// </summary>
        public DateTime FechaProduccion { get; set; }

        /// <summary>
        /// Nombre del usuario que realizó la producción.
        /// </summary>
        public string Usuario { get; set; }

        /// <summary>
        /// Nombre de la prenda producida.
        /// </summary>
        public string Prenda { get; set; }

        /// <summary>
        /// Tipo de prenda.
        /// </summary>
        public string TipoPrenda { get; set; }

        /// <summary>
        /// Color de la prenda.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Modelo de la prenda.
        /// </summary>
        public string Modelo { get; set; }

        /// <summary>
        /// Talla de la prenda.
        /// </summary>
        public string Talla { get; set; }

        /// <summary>
        /// Nombre de la operación realizada.
        /// </summary>
        public string Operacion { get; set; }

        /// <summary>
        /// Cantidad de operaciones realizadas.
        /// </summary>
        public int Cantidad { get; set; }

        /// <summary>
        /// Estado actual de la producción.
        /// </summary>
        public string Estado { get; set; }

        /// <summary>
        /// Total monetario asociado a la producción (puede ser nulo).
        /// </summary>
        public decimal? Total { get; set; }
    }
}
