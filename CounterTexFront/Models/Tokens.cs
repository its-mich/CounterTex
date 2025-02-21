using CounterTexFront.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounterTexFront.Models
{
    public class Tokens
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El ID de usuario es obligatorio.")]
        [Display(Name = "ID de Usuario")]
        public int UsuarioId { get; set; }

        public Usuarios Usuario { get; set; }

        [Required(ErrorMessage = "El token es obligatorio.")]
        [Display(Name = "Valor del Token")]
        public string TokenValue { get; set; }

        [Required(ErrorMessage = "La fecha de expiración es obligatoria.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de Expiración")]
        public DateTime Expira { get; set; }
    }
}
