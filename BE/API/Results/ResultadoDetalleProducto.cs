using API.Models;

namespace API.Results;

public class ResultadoDetalleProducto : ResultadoBase
{
    public List<ResultadoDetalleProductoItem> listaDetalleProducto { get; set; } = new List<ResultadoDetalleProductoItem>();
}

public class ResultadoDetalleProductoItem : ResultadoBase
{
    public Guid Id { get; set; }
    public int Cantidad { get; set; }
    public double PrecioInsumo { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public double Total { get; set; }
    public Insumo Insumo { get; set; }
    public Producto Producto { get; set; }
}
