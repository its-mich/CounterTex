using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class LoginResponse
	{
        public string Token { get; set; }
        public string Rol { get; set; }  // Cambio aquí
        public int Id { get; set; }
        public string Nombres { get; set; }

   
        public string Correo { get; set; }   // <<--- Asegúrate de tener esta
        public string Contraseña { get; set; }
        public int RolId { get; set; }
        public int Edad { get; set; }
        public string Telefono { get; set; }
        public string RolNombre { get; set; }

    }
}