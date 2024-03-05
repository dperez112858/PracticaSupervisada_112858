using API.Models;

namespace API.Results;

public class ResultadoFacturaUltimoAnio : ResultadoBase
{
    public List<ResultadoFacturaUltimoAnioItem> ListadoFactuasUltimoAnio {get; set;} = new List<ResultadoFacturaUltimoAnioItem>();
}

public class ResultadoFacturaUltimoAnioItem {
    public int mes {get; set;}
    public double montoFacturadoMensual {get; set;}
}