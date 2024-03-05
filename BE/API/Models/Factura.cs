namespace API.Models;

public class Factura
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
    public double Saldo {get; set;}
    public Cliente Cliente {get; set;}
}
