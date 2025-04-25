using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CounterTexFront.Models
{
    public class PerfilEmpleadoViewModel
    {
        [Key]
        public int IdEmpleado { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "La producción diaria debe ser un valor positivo.")]
        [Display(Name = "Producción Diaria")]
        public decimal ProduccionDiaria { get; set; }

        [Required(ErrorMessage = "El tipo de prenda es obligatorio.")]
        [Display(Name = "Tipo de Prenda")]
        public string TipoPrenda { get; set; }

        [Required(ErrorMessage = "El tipo de operación es obligatorio.")]
        [Display(Name = "Tipo de Operación")]
        public string TipoOperacion { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "La cantidad de operaciones no puede ser negativa.")]
        [Display(Name = "Cantidad de Operaciones")]
        public int CantidadOperacion { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El valor de la operación debe ser positivo.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Valor de Operación")]
        public decimal ValorOperacion { get; set; }

        [Required(ErrorMessage = "El campo de consulta de información es obligatorio.")]
        [Display(Name = "Consulta de Información")]
        public string ConsultarInformacion { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Control de Horarios")]
        public DateTime ControlHorarios { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Hora de Entrada")]
        public DateTime HoraEntrada { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Hora de Salida")]
        public DateTime HoraSalida { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "La meta por corte debe ser un valor positivo.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Meta por Corte")]
        public decimal MetaPorCorte { get; set; }

        [Required(ErrorMessage = "El botón de ayuda es obligatorio.")]
        [Display(Name = "Botón de Ayuda")]
        public string BotonAyuda { get; set; }

        [Display(Name = "Estadísticas")]
        public string Estadisticas { get; set; }

        [StringLength(500, ErrorMessage = "Las observaciones no pueden superar los 500 caracteres.")]
        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }

        [Required(ErrorMessage = "El ID de usuario es obligatorio.")]
        [Display(Name = "ID de Usuario")]
        public int IdUsuario { get; set; }

        public Usuarios Usuario { get; set; }
    }
}
