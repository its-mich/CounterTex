using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class RegistroViewModel
	{
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Documento { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public string ConfirmarContraseña { get; set; }
    }
}