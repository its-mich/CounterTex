using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CounterTexFront.Models
{
    public class ProduccionDiariaViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }

        [Display(Name = "Total Valor")]
        public decimal TotalValor { get; set; }

        [Display(Name = "Empleado")]
        public int UsuarioId { get; set; }

        [Display(Name = "Prenda")]
        public int PrendaId { get; set; }

        public List<ProduccionDiariaDetalleViewModel> ProduccionDetalles { get; set; } = new List<ProduccionDiariaDetalleViewModel>();

        public IEnumerable<SelectListItem> Usuarios { get; set; }
        public IEnumerable<SelectListItem> Prendas { get; set; }
        public IEnumerable<SelectListItem> Operaciones { get; set; }
    }

    public class ProduccionDiariaDetalleViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Cantidad de operacion")]
        public int Cantidad { get; set; }

        [Display(Name = " Nombre de la Operación")]
        public int OperacionId { get; set; }
    }

    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class Prenda
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class Operacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }


}
