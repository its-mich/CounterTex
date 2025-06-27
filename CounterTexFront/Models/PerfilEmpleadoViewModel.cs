using System;
using System.ComponentModel.DataAnnotations;

namespace CounterTexFront.Models
{
    /// <summary>
    /// ViewModel que representa el perfil detallado de un empleado dentro del sistema.
    /// </summary>
    public class PerfilEmpleadoViewModel
    {
        /// <summary>
        /// Identificador único del perfil del empleado.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nombre completo del empleado.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Correo electrónico del empleado.
        /// </summary>
        public string Correo { get; set; }

        /// <summary>
        /// Producción diaria registrada por el empleado.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "La producción diaria debe ser un valor positivo.")]
        [Display(Name = "Producción Diaria")]
        public decimal ProduccionDiaria { get; set; }

        /// <summary>
        /// Tipo de prenda asignado al empleado.
        /// </summary>
        [Required(ErrorMessage = "El tipo de prenda es obligatorio.")]
        [Display(Name = "Tipo de Prenda")]
        public string TipoPrenda { get; set; }

        /// <summary>
        /// Tipo de operación que realiza el empleado.
        /// </summary>
        [Required(ErrorMessage = "El tipo de operación es obligatorio.")]
        [Display(Name = "Tipo de Operación")]
        public string TipoOperacion { get; set; }

        /// <summary>
        /// Número de operaciones realizadas.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad de operaciones no puede ser negativa.")]
        [Display(Name = "Cantidad de Operaciones")]
        public int CantidadOperacion { get; set; }

        /// <summary>
        /// Valor económico de la operación realizada.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "El valor de la operación debe ser positivo.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Valor de Operación")]
        public decimal ValorOperacion { get; set; }

        /// <summary>
        /// Texto o módulo relacionado con la consulta de información.
        /// </summary>
        [Required(ErrorMessage = "El campo de consulta de información es obligatorio.")]
        [Display(Name = "Consulta de Información")]
        public string ConsultarInformacion { get; set; }

        /// <summary>
        /// Fecha y hora del control de horario.
        /// </summary>
        [DataType(DataType.DateTime)]
        [Display(Name = "Control de Horarios")]
        public DateTime ControlHorarios { get; set; }

        /// <summary>
        /// Hora de entrada del empleado.
        /// </summary>
        [DataType(DataType.Time)]
        [Display(Name = "Hora de Entrada")]
        public DateTime HoraEntrada { get; set; }

        /// <summary>
        /// Hora de salida del empleado.
        /// </summary>
        [DataType(DataType.Time)]
        [Display(Name = "Hora de Salida")]
        public DateTime HoraSalida { get; set; }

        /// <summary>
        /// Meta asignada al empleado por corte.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "La meta por corte debe ser un valor positivo.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Meta por Corte")]
        public decimal MetaPorCorte { get; set; }

        /// <summary>
        /// Enlace o nombre del botón de ayuda.
        /// </summary>
        [Required(ErrorMessage = "El botón de ayuda es obligatorio.")]
        [Display(Name = "Botón de Ayuda")]
        public string BotonAyuda { get; set; }

        /// <summary>
        /// Estadísticas o KPIs del rendimiento del empleado.
        /// </summary>
        [Display(Name = "Estadísticas")]
        public string Estadisticas { get; set; }

        /// <summary>
        /// Observaciones adicionales registradas.
        /// </summary>
        [StringLength(500, ErrorMessage = "Las observaciones no pueden superar los 500 caracteres.")]
        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }

        /// <summary>
        /// ID del usuario relacionado con el perfil del empleado.
        /// </summary>
        [Required(ErrorMessage = "El ID de usuario es obligatorio.")]
        [Display(Name = "ID de Usuario")]
        public int IdUsuario { get; set; }

        /// <summary>
        /// Información del usuario relacionado.
        /// </summary>
        public UsuarioViewModel Usuario { get; set; }

        /// <summary>
        /// Rol del empleado (nombre del rol).
        /// </summary>
        public string Rol { get; set; }
    }
}
