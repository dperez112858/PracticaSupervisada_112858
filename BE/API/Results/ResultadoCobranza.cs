using API.Models;

namespace API.Results;
public class ResultadoCobranza : ResultadoBase
{
    public ResultadoCobranzaItem data { get; set; } = new ResultadoCobranzaItem();
    public List<ResultadoCobranzaItem> listaCobranzas { get; set; } = new List<ResultadoCobranzaItem>();
}

public class ResultadoCobranzaItem
{
   public Guid Id { get; set; }
    public DateTime FechaLiquidacion { get; set; }
    public double Total { get; set; }
    public Cliente Cliente {get; set;}
    public List<DetalleCobranza> DetalleCobranza { get; set; }
}