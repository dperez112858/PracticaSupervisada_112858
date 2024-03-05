namespace API.Models;

public class DetallePresupuesto
{
    public Guid Id { get; set; }
    public int CantidadProductos { get; set; }
    // public double PrecioUnitario { get; set; }
    // public double Total { get; set; }
    public Guid PresupuestoId { get; set; }
    public Guid ProductoId { get; set; }
    public Producto? Producto { get; set; }
}
