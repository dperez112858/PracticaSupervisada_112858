using API.Models;

namespace API.Results;

public class ResultadoProducto : ResultadoBase
{
    public List<ResultadoProductoItem> listaProductos { get; set; } = new List<ResultadoProductoItem>();
}

public class ResultadoProductoItem
{
    public Guid Id { get; set; }
    public string Nombre { get; set; }
    public string? Banio { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public double CostoProducto { get; set; } //suma de los insumos
    public double Utilidad { get; set; } //ingresado por usuario
    public double CostoTotal { get; set; } // total suma insumos + utilidad 
}
