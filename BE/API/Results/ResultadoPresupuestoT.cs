using API.Models;

namespace API.Results;

public class ResultadoPresupuestoT: ResultadoBase
{
    public Guid Id { get; set; }
    public int Numero { get; set; }
    public string? Campania { get; set; }
    public double Total { get; set; }
    public bool Activo { get; set; }
    public bool Aceptado { get; set; }
    public Cliente Cliente { get; set; }
   

}
