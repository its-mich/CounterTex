using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class ContactoViewModel
	{
        [Required(ErrorMessage = "El Nombre Completo es requerido.")]
        [Display(Name = "Nombre Completo")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Correo Electrónico es requerido.")]
        [EmailAddress(ErrorMessage = "Por favor ingresa un correo electrónico válido.")]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }

        [Display(Name = "Teléfono (Opcional)")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El Asunto es requerido.")]
        [Display(Name = "Asunto")]
        public string Asunto { get; set; }

        [Required(ErrorMessage = "El Departamento es requerido.")]
        [Display(Name = "Departamento")]
        public string Departamento { get; set; }

        [Required(ErrorMessage = "Tu Mensaje es requerido.")]
        [Display(Name = "Tu Mensaje")]
        public string Mensaje { get; set; }

        [Required(ErrorMessage = "Debes aceptar la Política de Privacidad.")]
        [Display(Name = "Acepto la Política de Privacidad y el tratamiento de mis datos.")]
        public bool AceptaPrivacidad { get; set; }
    }
}