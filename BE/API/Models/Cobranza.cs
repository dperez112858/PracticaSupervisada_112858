namespace API.Models;

public class Cobranza
{
    public Guid Id { get; set; }
    public DateTime FechaLiquidacion { get; set; }
    public double Total { get; set; }
    public Cliente Cliente {get; set;}
    // public ICollection<DetalleCobranza> DetalleCobranza { get; set; }
    // public Cobranza()
    // {
    //     DetalleCobranza = new List<DetalleCobranza>();
    // }
}
