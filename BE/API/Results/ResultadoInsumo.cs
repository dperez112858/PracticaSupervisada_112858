using API.Models;

namespace API.Results;

public class ResultadoInsumo : ResultadoBase
{
    public List<ResultadoInsumoItem> listaInsumos { get; set; } = new List<ResultadoInsumoItem>();
}

public class ResultadoInsumoItem : ResultadoBase
{
    public Guid Id { get; set; }
    public string Descripcion { get; set; }
    public double Precio { get; set; }
    public bool Activo { get; set; }
    public DateTime FechaCompra { get; set; }
    public Proveedor Proveedor { get; set; }
}