using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CounterTexFront.Models
{
    /// <summary>
    /// ViewModel para registrar o mostrar una producción diaria.
    /// </summary>
    public class ProduccionDiariaViewModel
    {
        /// <summary>
        /// Identificador único de la producción.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Fecha en la que se realizó la producción.
        /// </summary>
        [Required(ErrorMessage = "La fecha es obligatoria.")]
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Valor total calculado de la producción.
        /// </summary>
        [Required(ErrorMessage = "El total es obligatorio.")]
        [Display(Name = "Total Valor")]
        [Range(0, double.MaxValue, ErrorMessage = "El total debe ser un valor positivo.")]
        public decimal TotalValor { get; set; }

        /// <summary>
        /// ID del empleado asociado a la producción.
        /// </summary>
        [Required(ErrorMessage = "Debe seleccionar un empleado.")]
        [Display(Name = "Empleado")]
        public int UsuarioId { get; set; }

        /// <summary>
        /// ID de la prenda producida.
        /// </summary>
        [Required(ErrorMessage = "Debe seleccionar una prenda.")]
        [Display(Name = "Prenda")]
        public int PrendaId { get; set; }

        /// <summary>
        /// Detalles de la producción por operación.
        /// </summary>
        public List<ProduccionDiariaDetalleViewModel> ProduccionDetalles { get; set; } = new List<ProduccionDiariaDetalleViewModel>();

        /// <summary>
        /// Lista de empleados disponibles (para combo).
        /// </summary>
        public IEnumerable<SelectListItem> Usuarios { get; set; }

        /// <summary>
        /// Lista de prendas disponibles (para combo).
        /// </summary>
        public IEnumerable<SelectListItem> Prendas { get; set; }

        /// <summary>
        /// Lista de operaciones disponibles (para combo).
        /// </summary>
        public IEnumerable<SelectListItem> Operaciones { get; set; }
    }

    /// <summary>
    /// Detalle individual de una operación realizada en la producción diaria.
    /// </summary>
    public class ProduccionDiariaDetalleViewModel
    {
        public int Id { get; set; }

        /// <summary>
        /// Cantidad realizada de la operación.
        /// </summary>
        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Display(Name = "Cantidad de operación")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero.")]
        public int Cantidad { get; set; }

        /// <summary>
        /// ID de la operación realizada.
        /// </summary>
        [Required(ErrorMessage = "Debe seleccionar una operación.")]
        [Display(Name = "Nombre de la Operación")]
        public int OperacionId { get; set; }
    }

    /// <summary>
    /// Representa un usuario/empleado relacionado con la producción.
    /// </summary>
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del usuario es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no debe superar los 100 caracteres.")]
        public string Nombre { get; set; }
    }

    /// <summary>
    /// Representa una prenda en el sistema.
    /// </summary>
    public class Prenda
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la prenda es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no debe superar los 100 caracteres.")]
        public string Nombre { get; set; }
    }

    /// <summary>
    /// Representa una operación que se puede realizar sobre una prenda.
    /// </summary>
    public class Operacion
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la operación es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no debe superar los 100 caracteres.")]
        public string Nombre { get; set; }
    }
}
