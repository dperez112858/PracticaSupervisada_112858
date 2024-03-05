using API.Models;
namespace API.Results;

public class ResultadoFactura : ResultadoBase
{
    public List<ResultadoFacturaItem> listaFacturas { get; set; } = new List<ResultadoFacturaItem>();
}
public class ResultadoFacturaItem
{
   public Guid Id { get; set; }
    public DateTime Fecha { get; set; }
    public string Numero {get; set;}
    public string TipoComprobante {get; set;} //1 FC, 3 NC
    public double NetoGravado {get; set;}
    public double Iva {get; set;}
    public bool? Dolar {get; set;} 
    public double? TipoCambio {get; set;} 
    public bool? Cancelada { get; set; } 
    public double Total { get; set; }
    public Cliente Cliente {get; set;}
    public double Saldo {get; set;}

}