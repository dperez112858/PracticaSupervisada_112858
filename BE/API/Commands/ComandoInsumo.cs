namespace API.Commands;

public class ComandoInsumo
{
    public Guid Id {get; set;}
    public string Descripcion { get; set; }
    public double Precio { get; set; }
    public DateTime FechaCompra { get; set; }
    public Guid ProveedorId { get; set; }
}
