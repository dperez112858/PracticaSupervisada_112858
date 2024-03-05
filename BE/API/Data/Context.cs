using Microsoft.EntityFrameworkCore;
using API.Models;
namespace API.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }
    public DbSet<Cliente> Cliente { get; set; }
    public DbSet<Proveedor> Proveedor { get; set; }
    public DbSet<Cobranza> Cobranza { get; set; }
    public DbSet<DetalleCobranza> DetalleCobranza { get; set; }
    public DbSet<CondicionIva> CondicionIva { get; set; }
    public DbSet<Insumo> Insumo { get; set; }
    public DbSet<Localidad> Localidad { get; set; }
    public DbSet<Presupuesto> Presupuesto { get; set; }
    public DbSet<Producto> Producto { get; set; }
    public DbSet<Provincia> Provincia { get; set; }
    public DbSet<TipoPago> TipoPago { get; set; }
    public DbSet<DetalleProducto> DetalleProducto { get; set; }
    public DbSet<DetallePresupuesto> DetallePresupuesto { get; set; }
    public DbSet<Factura> Factura { get; set; }
    public DbSet<CondicionIva> condicionIva {get; set;}
}
