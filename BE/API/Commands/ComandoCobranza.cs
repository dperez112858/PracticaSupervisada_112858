using API.Models;
namespace API.Commands;

public class ComandoCobranza
{
    public Guid Id { get; set; }
    public DateTime FechaLiquidacion { get; set; }
    public double Total { get; set; }
    public Guid ClienteId { get; set; }
    public List<DetalleCobranza> detalleCobranza { get; set; }
}