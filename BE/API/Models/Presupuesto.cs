using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class Presupuesto
{ 
    public Guid Id { get; set; }
    public string? campania { get; set; }
    public double Total { get; set; }
    // public Guid? ClienteId { get; set; }
    public bool Activo { get; set; }
    public bool Aceptado { get; set; }
    public Cliente? Cliente { get; set; }
    public DateTime Fecha { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Numero { get; set; }
}