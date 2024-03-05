namespace API.Models;

public class Localidad
{
    public Guid Id { get; set; }
    public string Nombre { get; set; }
    public Provincia Provincia { get; set; }
}
