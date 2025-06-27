using CounterTexFront.Models.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CounterTexFront.Models
{
    public class ProduccionDiariaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El total es obligatorio.")]
        [Display(Name = "Total Valor")]
        [Range(0, double.MaxValue, ErrorMessage = "El total debe ser un valor positivo.")]
        public decimal TotalValor { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un empleado.")]
        [Display(Name = "Empleado")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una prenda.")]
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

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Display(Name = "Cantidad de operación")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una operación.")]
        [Display(Name = "Nombre de la Operación")]
        public int OperacionId { get; set; }
    }

    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo no es válido.")]
        public string Correo { get; set; }

        public string Contraseña { get; set; }

        public string Documento { get; set; }

        public int RolId { get; set; }

        public int? Edad { get; set; }

        [StringLength(15, ErrorMessage = "Máximo 15 caracteres.")]
        public string Telefono { get; set; }
    }

    public class Prenda
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la prenda es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no debe superar los 100 caracteres.")]
        public string Nombre { get; set; }
    }

    public class Operacion
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la operación es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no debe superar los 100 caracteres.")]
        public string Nombre { get; set; }
    }


    //public class ProduccionApiDto
    //{
    //    public int Id { get; set; }
    //    public string Fecha { get; set; }
    //    public string PrendaNombre { get; set; }  // <-- Esto es lo que necesitabas
    //    public decimal? TotalValor { get; set; }
    //    public List<ProduccionDetalleDto> ProduccionDetalles { get; set; }
    //}
}
