using Newtonsoft.Json;        // Para JsonConvert y serialización/deserialización
using Newtonsoft.Json.Linq;   // Para JObject, JArray, etc. si lo necesitas
using System.Linq;            // Para LINQ
using System;                 // Para tipos básicos como DateTime
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace CounterTexFront.Models
{
    public class ProduccionDiariaViewModel
    {
        //constructor por defecto
        public ProduccionDiariaViewModel()
        {
            ProduccionDetalles = new List<ProduccionDetalleViewModel>();
            UsuariosDisponibles = new List<UsuarioViewModel>();
            PrendasDisponibles = new List<PrendaViewModel>();
            OperacionesDisponibles = new List<OperacionViewModel>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        [DataType(DataType.Date)] // Para que el input de fecha se muestre mejor
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El usuario es obligatorio.")]
        [Display(Name = "Usuario")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "La prenda es obligatoria.")]
        [Display(Name = "Prenda")]
        public int PrendaId { get; set; }

        // ¡DESCOMENTAR O AÑADIR ESTA PROPIEDAD!
        // Tu API devuelve 'totalValor' en el JSON de detalle, por eso la vista lo espera.
        public decimal TotalValor { get; set; }

        // Importante: El nombre de la propiedad debe coincidir con el nombre de la colección en tu entidad 'Produccion' en la API
        public List<ProduccionDetalleViewModel> ProduccionDetalles { get; set; }

        // Propiedades para rellenar DropDownLists en la vista (no se envían a la API)
        public IEnumerable<UsuarioViewModel> UsuariosDisponibles { get; set; }
        public IEnumerable<PrendaViewModel> PrendasDisponibles { get; set; }
        public IEnumerable<OperacionViewModel> OperacionesDisponibles { get; set; }

        // Propiedades de navegación para la vista de Detalles (si la API las devuelve)
        // Basado en el JSON que me mostraste, tu API sí devuelve estos objetos completos.
        public UsuarioViewModel Usuario { get; set; }
        public PrendaViewModel Prenda { get; set; }
    }
    public class PrendaViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        // Añade otras propiedades si tu API las devuelve y las necesitas (ej. Genero, Color)
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