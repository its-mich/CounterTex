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

        public string NombreRol { get; set; }

        [Display(Name = "Nuevo Rol")]
        [Required(ErrorMessage = "Debe seleccionar un rol.")]
        public int? NuevoRolId { get; set; }

        public List<RolViewModel> RolesDisponibles { get; set; }
    }
}