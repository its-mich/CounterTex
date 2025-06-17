using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CounterTexFront.Models
{
    public class ProduccionDiariaViewModel
    {
        // Constructor por defecto
        public ProduccionDiariaViewModel()
        {
            ProduccionDetalles = new List<ProduccionDetalleViewModel>();
            UsuariosDisponibles = new List<UsuarioViewModel>();
            PrendasDisponibles = new List<PrendaViewModel>();
            OperacionesDisponibles = new List<OperacionViewModel>();
            ListaProducciones = new List<ProduccionListadoViewModel>();
        }

        // Datos de Producción
        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El usuario es obligatorio.")]
        [Display(Name = "Usuario")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "La prenda es obligatoria.")]
        [Display(Name = "Prenda")]
        public int PrendaId { get; set; }

        public decimal TotalValor { get; set; }

        // Detalles de producción
        public List<ProduccionDetalleViewModel> ProduccionDetalles { get; set; }

        // Para combos en la vista
        public IEnumerable<UsuarioViewModel> UsuariosDisponibles { get; set; }
        public IEnumerable<PrendaViewModel> PrendasDisponibles { get; set; }
        public IEnumerable<OperacionViewModel> OperacionesDisponibles { get; set; }

        // Para mostrar registros existentes
        public List<ProduccionListadoViewModel> ListaProducciones { get; set; }

        // Paginación
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }

        // Propiedades navegacionales si se devuelven en detalle
        public UsuarioViewModel Usuario { get; set; }
        public PrendaViewModel Prenda { get; set; }
    }

  
    public class ProduccionListadoViewModel
    {
        public int Id { get; set; }
        public string TipoPrenda { get; set; }
        public string Color { get; set; }
        public string Modelo { get; set; }
        public string Talla { get; set; }
        public string Operacion { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaProduccion { get; set; }
        public string Estado { get; set; }
    }

  

    public class PrendaViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public string Color { get; set; }
    }

    public class OperacionViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal? ValorUnitario { get; set; }
    }
}
