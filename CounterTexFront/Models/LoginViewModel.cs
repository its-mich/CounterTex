using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
    public class LoginViewModel
    {
        [DisplayName("Correo")]
        [Required(ErrorMessage = "El campo {0} es requerido", AllowEmptyStrings = false)]
        [EmailAddress]
        [JsonProperty("Correo")]  // Esto es lo que hace la magia
        public string UserName { get; set; }

        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "El campo {0} es requerido", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [JsonProperty("Contraseña")]  // Esto lo cambia de "Password" a "Clave" en el JSON
        public string Password { get; set; }
    }
}