namespace API.Models;

public class Producto
{
    public Guid Id { get; set; }
    public string banioProducto { get; set; }
    public string nombreProducto { get; set; }
    public int cantidadProductos { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public double costoProducto { get; set; } //suma de los insumos
    public double utilidad { get; set; } //ingresado por usuario
    public double costoTotal { get; set; } // total suma insumos + utilidad 
    public Guid PresupuestoId { get; set; }
    public virtual List<DetalleProducto> DetalleProductos { get; set; }
}
