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
    }
}