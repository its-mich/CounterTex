using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
    /// <summary>
    /// Modelo de vista para cambiar el rol de un usuario.
    /// </summary>
    public class CambiarRolViewModel
    {
        /// <summary>
        /// Identificador único del usuario.
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        [Display(Name = "Nombre del Usuario")]
        public string Nombre { get; set; }

        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        [EmailAddress(ErrorMessage = "Ingrese un correo válido.")]
        [Display(Name = "Correo Electrónico")]
        public string Correo { get; set; }

        /// <summary>
        /// Nombre del rol actual del usuario.
        /// </summary>
        [Display(Name = "Rol Actual")]
        public string RolActual { get; set; }

        /// <summary>
        /// Identificador del nuevo rol seleccionado.
        /// </summary>
        [Required(ErrorMessage = "Debe seleccionar un nuevo rol.")]
        [Display(Name = "Nuevo Rol")]
        public int NuevoRolId { get; set; }

        /// <summary>
        /// Lista de roles disponibles para asignar al usuario.
        /// </summary>
        [Display(Name = "Roles Disponibles")]
        public List<RolViewModel> RolesDisponibles { get; set; }
    }
}