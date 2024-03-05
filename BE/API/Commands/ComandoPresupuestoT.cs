using API.Models;

namespace API.Commands;

public class ComandoPresupuestoT
{
    public string? Campania { get; set; }
    public double Total { get; set; }
    public bool Activo { get; set; }
    public bool Aceptado { get; set; }
    public Guid ClienteId { get; set; }
    
}
