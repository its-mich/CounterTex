using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class LoginResponse
	{
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("rol")]
        public string Rol { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nombres")]
        public string Nombres { get; set; }

        [JsonProperty("apellidos")]
        public string Apellidos { get; set; }
    }
}