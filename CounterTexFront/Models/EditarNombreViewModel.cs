using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class EditarNombreViewModel
	{

        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public int RolId { get; set; }
        public int Edad { get; set; }
        public string Telefono { get; set; }
        public string RolNombre { get; set; }

    }
}