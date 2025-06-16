using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class CambiarRolViewModel
	{
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Correo { get; set; }

        public string RolActual { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un nuevo rol.")]
        [Display(Name = "Nuevo Rol")]
        public int NuevoRolId { get; set; }

        public List<RolViewModel> RolesDisponibles { get; set; }
    }
}