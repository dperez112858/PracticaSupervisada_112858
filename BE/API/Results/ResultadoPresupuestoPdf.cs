using API.Models;

namespace API.Results;

public class ResultadoPresupuestoPdf : ResultadoBase
{
    public Guid Id { get; set; }
    public int Numero { get; set; }
    public string? Campania { get; set; }
    public double Total { get; set; }
    public bool Aceptado { get; set; }
    public Cliente Cliente { get; set; }
    public List<ResultadoItem> Items { get; set; } = new List<ResultadoItem>();
}

public class ResultadoItem
{
    public string Producto { get; set; }
    public double Total { get; set; }
    public int Cantidad { get; set; }
}
