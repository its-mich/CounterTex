using CounterTexFront.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounterTexFront.Models
{
    /// <summary>
    /// Modelo que representa un token de autenticación y su rol asociado.
    /// </summary>
    public class Tokens
    {
        /// <summary>
        /// Valor del token único asignado al usuario.
        /// </summary>
        [Key]
        public string TokenValue { get; set; }

        /// <summary>
        /// Rol asignado al token, utilizado para autorizar acceso.
        /// </summary>
        public string Rol { get; set; }
    }
}
