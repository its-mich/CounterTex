using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class LoginResponse
	{
        public int Id { get; set; }
        public string Nombres { get; set; }

        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public string Telefono { get; set; }
        public int Edad { get; set; }
        public int RolId { get; set; }
        public string RolNombre { get; set; }
        public string Documento { get; set; }
        public string Rol { get; set; } // Opcional, si no usas ambos Rol y RolNombre, puedes quitar uno.
        public string Token { get; set; }

    }
}