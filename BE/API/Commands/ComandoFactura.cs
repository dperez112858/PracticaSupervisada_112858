using API.Models;

namespace API.Commands;

public class ComandoFactura
{
    public Guid Id { get; set; }
    public DateTime Fecha {get; set;}
    public string Numero { get; set; }
    public string TipoComprobante { get; set; }
    public double NetoGravado { get; set; }
    public double Iva { get; set; }
    public bool Dolar { get; set; }
    public double TipoCambio {get; set;}
    public double Total { get; set; }
    public bool? Cancelada {get; set;}
    public Guid ClienteId {get; set;}
}
