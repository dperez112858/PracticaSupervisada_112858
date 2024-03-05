using API.Models;
namespace API.Results;

public class ResultadoProvincia : ResultadoBase
{
    public Guid Id { get; set; }
    public string Nombre { get; set; }
}
