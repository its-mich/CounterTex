using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
	public class Usuario
	{

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombres { get; set; }

        [StringLength(100)]
        public string Apellidos { get; set; }

        [Required]
        [StringLength(20)]
        public string Documento { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Correo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(255)]
        public string Contraseña { get; set; }

        [Required]
        [StringLength(20)]
        public string Rol { get; set; }

        [Range(0, 100)]
        public int Edad { get; set; }

        [StringLength(20)]
        public string Telefono { get; set; }
    }
}