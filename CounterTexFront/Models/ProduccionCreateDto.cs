using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
    public class ProduccionCreateDto
    {
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public int PrendaId { get; set; }
        public List<ProduccionDetalleCreateDto> ProduccionDetalles { get; set; }
    }

    // DTO para ENVIAR datos de detalle de creación a la API
    public class ProduccionDetalleCreateDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Cantidad { get; set; }
        [Required]
        public int OperacionId { get; set; }
    }
}