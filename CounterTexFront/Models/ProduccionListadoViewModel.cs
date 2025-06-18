using System;

public class ProduccionListadoViewModel
{
    public int Id { get; set; }
    public DateTime FechaProduccion { get; set; }
    public string NombreUsuario { get; set; }  // <-- Asegúrate de tener esta
    public string NombrePrenda { get; set; }   // <-- Y esta
    public int Total { get; set; }

}
