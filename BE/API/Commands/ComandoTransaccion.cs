using System.Globalization;
using API.Models;

namespace API.Commands;

public class ComandoTransaccion
{
    public Guid ClienteId { get; set; }
    public string campania { get; set; }
    public bool Aceptado { get; set; }
    public bool Activo { get; set; }

    public Producto[] productos { get; set; }
}
