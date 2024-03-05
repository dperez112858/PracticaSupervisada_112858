namespace API.Properties;

public class ComandoFactura
{
    public Guid Id { get; set; }
    public DateTime Fecha {get; set;}
    public string Numero { get; set; }
    public int TipoComprobante { get; set; }
    public double NetoGravado { get; set; }
    public double Iva { get; set; }
    public bool Dolar { get; set; }

}
