using API.Models;

namespace API.Commands;

public class ComandoPresupuestoU
{
    public string? campania { get; set; }
    public double? Total { get; set; }
    public bool? Activo { get; set; }
    public bool? Aceptado { get; set; }
    public Guid clienteId { get; set; }
    public List<Producto> productos {get; set;}
    
}
