using API.Models;

namespace API.Commands;

public class ComandoProducto
{
    public Guid Id { get; set; }
    public string Nombre { get; set; }
    public string? Banio { get; set; }
    public DateTime? FechaCreacion { get; set; }
    //public double Cantidad { get; set; }
    public double CostoProducto { get; set; }
    public double Utilidad { get; set; } 
    public double CostoTotal { get; set; }
}
