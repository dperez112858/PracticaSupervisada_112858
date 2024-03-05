using API.Models;

namespace API.Commands;

public class ComandoDetalleProducto
{
    //DetalleProducto
    public Guid Id { get; set; }
    public int Cantidad { get; set; }
    public double PrecioInsumo { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public double Total { get; set; }
    public Guid InsumoId { get; set; }
    public Guid ProductoId { get; set; }
}
