using API.Models;

namespace API.Results;

public class ResultadoPresupuesto : ResultadoBase
{
    public List<ResultadoPresupuestoItem> listaPresupuestos { get; set; } = new List<ResultadoPresupuestoItem>();
}

public class ResultadoPresupuestoItem
{
    public Guid Id { get; set; }
    public int Numero { get; set; }
    public string? Campania { get; set; }
    public double Total { get; set; }
    public bool Activo { get; set; }
    public bool Aceptado { get; set; }
    public Cliente Cliente { get; set; }
}
