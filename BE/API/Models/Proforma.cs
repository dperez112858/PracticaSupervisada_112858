namespace API.Models;

public class Proforma
{
    public Guid Id { get; set; }
    public DateTime Fecha { get; set; }
    public string Numero {get; set;}
    public double NetoGravado {get; set;}
    public double Iva {get; set;}
    public int? Moneda {get; set;} 
    public double? TipoCambio {get; set;}  
    public double Total { get; set; }
    public Cliente Cliente {get; set;}
    public Presupuesto Presupuesto {get; set;}
}
