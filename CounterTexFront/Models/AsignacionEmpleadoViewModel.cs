using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class AsignacionEmpleadoViewModel
	{
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "El tipo de prenda es obligatorio.")]
        [Display(Name = "Tipo de Prenda")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(200, ErrorMessage = "La descripción no debe exceder los 200 caracteres.")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La prioridad es obligatoria.")]
        public string Prioridad { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public string Estado { get; set; }

        [Display(Name = "Asignado A")]
        public int? OperadorId { get; set; } // Usado al asignar prenda

        [StringLength(500, ErrorMessage = "Los comentarios no deben exceder los 500 caracteres.")]
        public string Comentarios { get; set; }

    }
}