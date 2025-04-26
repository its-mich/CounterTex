
using System.ComponentModel.DataAnnotations;

namespace CounterTexFront.Models
{
    public class PerfilAdministradorViewModel
    {
        [Key] //llave primaria
        public int IdAdministrador { get; set; }

        [Required(ErrorMessage = "El nombre del administrador es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no debe superar los 100 caracteres.")]
        [Display(Name = "Nombre del Administrador")]
        public string NombreAdministrador { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "La producción diaria no puede ser negativa.")]
        [Display(Name = "Producción Diaria")]
        public int ProduccionDiaria { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "La producción mensual no puede ser negativa.")]
        [Display(Name = "Producción Mensual")]
        public int ProduccionMensual { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El control de prendas no puede ser negativo.")]
        [Display(Name = "Control de Prendas")]
        public int ControlPrendas { get; set; }

        [Required(ErrorMessage = "El registro es obligatorio.")]
        [StringLength(200, ErrorMessage = "El registro no debe superar los 200 caracteres.")]
        [Display(Name = "Registro de Actividades")]
        public string Registro { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Las ganancias deben ser un valor positivo.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Ganancias")]
        public decimal Ganancias { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Los pagos deben ser un valor positivo.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Pagos Realizados")]
        public decimal Pagos { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Los gastos deben ser un valor positivo.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Gastos Generales")]
        public decimal Gastos { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "La meta por corte debe ser un valor positivo.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Meta por Corte")]
        public decimal MetaPorCorte { get; set; }

        [Required(ErrorMessage = "El campo de consulta de información es obligatorio.")]
        [Display(Name = "Consulta de Información")]
        public string ConsultarInformacion { get; set; }

        [Required(ErrorMessage = "El control de horarios es obligatorio.")]
        [Display(Name = "Control de Horarios")]
        public string ControlHorarios { get; set; }

        [Required(ErrorMessage = "El chat interno es obligatorio.")]
        [Display(Name = "Chat Interno")]
        public string ChatInterno { get; set; }

        [Required(ErrorMessage = "El proveedor es obligatorio.")]
        [Display(Name = "Proveedor")]
        public string Proveedor { get; set; }

        [Required(ErrorMessage = "El botón de ayuda es obligatorio.")]
        [Display(Name = "Botón de Ayuda")]
        public string BotonAyuda { get; set; }

        [Required(ErrorMessage = "El ID de usuario es obligatorio.")]
        [Display(Name = "ID de Usuario")]
        public int IdUsuario { get; set; }
    }
}
