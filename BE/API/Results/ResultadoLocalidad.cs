using API.Models;
namespace API.Results;
public class ResultadoLocalidad : ResultadoBase
{
    public List<ResultadoLocalidadItem> listaLocalidades { get; set; } = new List<ResultadoLocalidadItem>();

}

public class ResultadoLocalidadItem
{
    public Guid Id { get; set; }
    public string Nombre { get; set; }
    public Provincia Provincia { get; set; }
}