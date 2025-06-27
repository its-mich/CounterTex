using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
    /// <summary>
    /// Representa la respuesta devuelta al iniciar sesión exitosamente.
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// Identificador único del usuario.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombres completos del usuario.
        /// </summary>
        public string Nombres { get; set; }

        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        public string Correo { get; set; }

        /// <summary>
        /// Contraseña (generalmente omitida en respuestas por seguridad).
        /// </summary>
        public string Contraseña { get; set; }

        /// <summary>
        /// Número de teléfono del usuario.
        /// </summary>
        public string Telefono { get; set; }

        /// <summary>
        /// Edad del usuario.
        /// </summary>
        public int Edad { get; set; }

        /// <summary>
        /// Identificador del rol asociado al usuario.
        /// </summary>
        public int RolId { get; set; }

        /// <summary>
        /// Nombre del rol (por ejemplo: Administrador, Empleado).
        /// </summary>
        public string RolNombre { get; set; }

        /// <summary>
        /// Número de documento del usuario.
        /// </summary>
        public string Documento { get; set; }

        /// <summary>
        /// Rol del usuario (opcional si se usa RolNombre).
        /// </summary>
        public string Rol { get; set; }

        /// <summary>
        /// Token JWT de autenticación generado al iniciar sesión.
        /// </summary>
        public string Token { get; set; }
    }
}
