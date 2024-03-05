namespace API.Models;

public class DetalleCobranza
{
    public Guid Id { get; set; }
    public double ImporteCobrado { get; set; }
    public double ImporteRetenido { get; set; }
    public double ImporteTotal { get; set; }
    public Guid FacturaId { get; set; }
    public DateTime FechaPago { get; set; }
    public Guid TipoPagoId { get; set; }
    public Guid CobranzaId { get; set; }
}
