using System.Security.Principal;
namespace API.Models;

public class Insumo
{
    public Guid Id { get; set; }
    public string Descripcion { get; set; } = " ";
    public double Precio { get; set; }
    public bool Activo {get; set;}
    public DateTime FechaCompra { get; set; }
    public Proveedor Proveedor { get; set; }
}
