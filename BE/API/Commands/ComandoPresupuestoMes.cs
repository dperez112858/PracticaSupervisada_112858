namespace API.Commands;

public class ComandoPresupuestoMes
{
    public DateTime fechaInicio {get; set;}
    public DateTime fechaFin {get; set;}
    public bool cancelada {get; set;}
    public Guid ClienteId { get; set; }
}
