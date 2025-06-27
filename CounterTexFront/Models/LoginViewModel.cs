using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
    /// <summary>
    /// Modelo para capturar las credenciales del usuario durante el inicio de sesión.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Correo electrónico del usuario (usado como nombre de usuario).
        /// </summary>
        [DisplayName("Correo")]
        [Required(ErrorMessage = "El campo {0} es requerido", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo válido.")]
        [JsonProperty("Correo")]
        public string UserName { get; set; }

        /// <summary>
        /// Contraseña del usuario.
        /// </summary>
        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "El campo {0} es requerido", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [JsonProperty("Contraseña")]
        public string Password { get; set; }
    }
}
