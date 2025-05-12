using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class AsignacionViewModel
	{
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El tipo de prenda es obligatorio")]
        [Display(Name = "Tipo de Prenda")]
        public string TipoPrenda { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "La prioridad es obligatoria")]
        [Display(Name = "Prioridad")]
        public string Prioridad { get; set; }

        [Required(ErrorMessage = "Debe asignarse a alguien")]
        [Display(Name = "Asignar a")]
        public string AsignadoA { get; set; }

        [Display(Name = "Comentarios")]
        [DataType(DataType.MultilineText)]
        public string Comentarios { get; set; }
    }
}