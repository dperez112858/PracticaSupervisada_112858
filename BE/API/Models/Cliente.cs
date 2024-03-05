namespace API.Models;

public class Cliente
{
    public Guid Id { get; set; }
    public string RazonSocial { get; set; }
    public string Cuit { get; set; }
    public string Calle { get; set; }
    public string Numero { get; set; }
    public string? CodigoPostal { get; set; }
    public string? Telefono { get; set; }
    public string? NombreContacto { get; set; }
    public string? Email { get; set; }
    public string? Comentario { get; set; }
    public bool Activo {get; set;}
    public decimal Saldo { get; set;}
    public Localidad? Localidad {get; set;}
    public CondicionIva CondicionIva { get; set; }
}
