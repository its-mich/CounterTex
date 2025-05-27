using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class RevisionEmpleadoViewModel
	{

        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El tipo de prenda es obligatorio.")]
        [StringLength(50, ErrorMessage = "El tipo de prenda no puede exceder los 50 caracteres.")]
        [Display(Name = "Tipo de Prenda")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(200, ErrorMessage = "La descripción no puede exceder los 200 caracteres.")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El operador es obligatorio.")]
        [StringLength(100, ErrorMessage = "El operador no puede exceder los 100 caracteres.")]
        [Display(Name = "Operador")]
        public string Operador { get; set; }

        [Required(ErrorMessage = "El estado de revisión es obligatorio.")]
        [StringLength(50, ErrorMessage = "El estado no puede exceder los 50 caracteres.")]
        [Display(Name = "Estado de Revisión")]
        // Ejemplos: "Por Revisar", "Aprobado", "Rechazado"
        public string Estado { get; set; }

        [StringLength(500, ErrorMessage = "Los comentarios no pueden exceder los 500 caracteres.")]
        [Display(Name = "Comentarios de Revisión")]
        [DataType(DataType.MultilineText)] // Sugiere un área de texto multilinea para la UI
        public string Comentario { get; set; }
    }
}