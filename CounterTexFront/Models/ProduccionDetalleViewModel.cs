using System.ComponentModel.DataAnnotations;

namespace CounterTexFront.Models
{
	public class ProduccionDetalleViewModel
	{
        public int Id { get; set; }

        [Display(Name = "Cantidad")]
        public int Cantidad { get; set; }

        [Display(Name = "Operación")]
        public int OperacionId { get; set; }
    }
}