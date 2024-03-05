namespace API.Models;

public class DetalleProducto
{
    public Guid Id { get; set; }
    public int Cantidad {get; set;}
    public DateTime? FechaCreacion {get; set;} 
    public double precioInsumo { get; set; }
    public double total {get; set;}
    public Guid InsumoId { get; set; }
    public Guid? ProductoId { get; set; }
}
