using System.Reflection.Metadata;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using Microsoft.EntityFrameworkCore;
using API.Results;
using API.Models;
using API.Commands;

namespace API.Controllers;
[Route("api/[controller]")]

public class PresupuestoController : ControllerBase
{
    private readonly Context _context;

    public PresupuestoController(Context context)
    {
        _context = context;
    }

    [HttpPost("Crear")]
    public async Task<ActionResult<ResultadoBase>> Crear([FromBody] ComandoTransaccion comando)
    {
        var resultado = new ResultadoBase();

        Console.WriteLine(comando);

        try
        {
            if (comando.ClienteId != null)
            {
                var cliente = await _context.Cliente.
                    Include(v => v.CondicionIva).
                    Include(l => l.Localidad).
                    Include(p => p.Localidad.Provincia).
                    Where(c => c.Id.Equals(comando.ClienteId)).FirstOrDefaultAsync();

                var total = 0.0;
                foreach (Producto producto in comando.productos)
                {
                    total += producto.DetalleProductos.Sum(c => c.Cantidad * c.precioInsumo);
                }
                var presupuesto = new Presupuesto()
                {
                    Id = Guid.NewGuid(),
                    campania = comando.campania,
                    Aceptado = comando.Aceptado,
                    Activo = comando.Activo,
                    Total = total,
                    Cliente = cliente
                };
                await GuardarPresupuesto2(presupuesto, comando.productos);
            }


            // await _context.SaveChangesAsync();
            resultado.StatusCode = 200;
            return Ok(resultado);

        }
        catch (Exception ex)
        {

            resultado.SetError("Error al crear el presupuesto" + ex.Message);
            resultado.StatusCode = 500;
            return Ok(resultado);
        }
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public Task GuardarPresupuesto2(Presupuesto presupuesto, IList<Producto> productos)
    {

        _context.Presupuesto.Add(presupuesto);
        return _context.SaveChangesAsync().ContinueWith(async t =>
        {

            foreach (var pro in productos)
            {

                var producto = new Producto()
                {
                    Id = Guid.NewGuid(),
                    banioProducto = pro.banioProducto,
                    nombreProducto = pro.nombreProducto,
                    cantidadProductos = pro.cantidadProductos,
                    FechaCreacion = DateTime.Now,
                    costoProducto = pro.costoProducto,
                    utilidad = pro.costoProducto * 0.4,
                    costoTotal = pro.costoTotal,
                    PresupuestoId = presupuesto.Id
                };

                await GuardarProducto2(producto, presupuesto.Id, pro.DetalleProductos);

            }
        }
        );
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public Task GuardarProducto2(Producto producto, Guid pId, IList<DetalleProducto> detalles)
    {

        _context.Producto.Add(producto);
        return _context.SaveChangesAsync().ContinueWith(async t =>
        {
            // var detallePresupuesto = new DetallePresupuesto(){
            //         Id = Guid.NewGuid(),
            //         PresupuestoId = pId,
            //         ProductoId = producto.Id
            //     };

            //     await _context.DetallePresupuesto.AddAsync(detallePresupuesto);

            var tasks = new List<Task>();

            foreach (var detalle in detalles)
            {
                // await GuardarDetalleProducto(detalle, producto.Id);
                tasks.Add(GuardarDetalleProducto(detalle, producto.Id));
            }
            await Task.WhenAll(tasks);
        });

    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task GuardarDetalleProducto(DetalleProducto detalle, Guid pId)
    {
        var detalleProductos = new DetalleProducto()
        {
            Id = Guid.NewGuid(),
            Cantidad = detalle.Cantidad,
            FechaCreacion = DateTime.Now,
            precioInsumo = detalle.precioInsumo,
            total = detalle.total,
            InsumoId = detalle.InsumoId,
            ProductoId = pId
        };
        _context.DetalleProducto.Add(detalleProductos);
        await _context.SaveChangesAsync();

    }




    // [HttpPost("Crear")]
    // public async Task<ActionResult<ResultadoBase>> Crear([FromBody] ComandoTransaccion comando)
    // {
    //     var resultado = new ResultadoBase();
    //     var cli = await _context.Cliente.Where(c => c.Id.Equals(comando.clienteId)).FirstOrDefaultAsync();
    //     try
    //     {
    //         var presupuesto = new Presupuesto()
    //         {
    //             Id = Guid.NewGuid(),
    //             Cliente = cli,
    //             campania = comando.campania,
    //             Total = comando.detallePresupuestos.Sum(c => c.PrecioUnitario * c.CantidadProductos)
    //         };
    //         foreach (var productos in comando.detallePresupuestos)
    //         {
    //             var detallePresupuesto = new DetallePresupuesto()
    //             {
    //                 Id = Guid.NewGuid(),
    //                 CantidadProductos = productos.CantidadProductos,
    //                 PrecioUnitario = productos.PrecioUnitario,
    //                 Total = productos.CantidadProductos * productos.PrecioUnitario,
    //                 PresupuestoId = presupuesto.Id,
    //                 ProductoId = Guid.NewGuid(),
    //             };

    //             if (detallePresupuesto != null)
    //             {
    //                 await _context.DetallePresupuesto.AddAsync(detallePresupuesto);
    //             }
    //             else
    //             {
    //                 resultado.SetError("Error, el detalle de cobranza cargó nulo");
    //                 resultado.StatusCode = 500;
    //                 return Ok(resultado);
    //             }

    //         }


    // var presupuestoT = new Presupuesto()
    // {
    //     Id = comando.PresupuestoT.Id,
    //     Campania = comando.PresupuestoT.Campania,
    //     Total = comando.PresupuestoT.Total,
    //     Activo = comando.PresupuestoT.Activo,
    //     Aceptado = comando.PresupuestoT.Aceptado,
    //     Cliente = comando.PresupuestoT.Cliente,
    //     Numero = comando.PresupuestoT.Numero,
    //     DetallePresupuesto = new List<DetallePresupuesto>()
    // };

    // foreach (var detallePresupuestoT in comando.PresupuestoT.DetallePresupuesto)
    // {
    //     var detallePresupuesto = new DetallePresupuesto()
    //     {
    //         Id = detallePresupuestoT.Id,
    //         CantidadProductos = detallePresupuestoT.CantidadProductos,
    //         PrecioUnitario = detallePresupuestoT.PrecioUnitario,
    //         Presupuesto = comando.PresupuestoT,
    //         Producto = comando.ProductoT
    //     };

    //     if (detallePresupuesto != null) {
    //         await _context.DetallePresupuesto.AddAsync(detallePresupuesto);
    //     } else
    //     {
    //         resultado.SetError("Error, el detalle de cobranza cargó nulo");
    //         resultado.StatusCode = 500;
    //         return Ok(resultado);
    //     }
    // }

    // await _context.Producto.AddAsync(productoT);
    // await _context.Presupuesto.AddAsync(presupuestoT);
    //         await _context.SaveChangesAsync();
    //         resultado.StatusCode = 200;
    //         return Ok(resultado);

    //     }
    //     catch (Exception ex)
    //     {

    //         resultado.SetError("Error al crear el presupuesto");
    //         resultado.StatusCode = 500;
    //         return Ok(resultado);
    //     }
    // }


    // [HttpPost("Crear")]
    // public async Task<ActionResult<ResultadoBase>> Crear([FromBody] ComandoTransaccion comando)
    // {
    //     var resultado = new ResultadoBase();
    //     var cli = await _context.Cliente.Where(c => c.Id.Equals(comando.clienteId)).FirstOrDefaultAsync();
    //     try
    //     {
    //         var presupuesto = new Presupuesto()
    //         {
    //             Id = Guid.NewGuid(),
    //             Cliente = cli,
    //             campania = comando.campania,
    //             Total = 0
    //         };
    //          foreach (var detalle in comando.detallePresupuestos)
    //         {
    //             var detallePresupuesto = new DetallePresupuesto()
    //             {
    //                 Id = Guid.NewGuid(),
    //                 CantidadProductos = detalle.CantidadProductos,
    //                 PrecioUnitario = detalle.PrecioUnitario,
    //                 Total = detalle.CantidadProductos * detalle.PrecioUnitario,
    //                 PresupuestoId = presupuesto.Id,
    //                 ProductoId = Guid.NewGuid(),
    //             };

    //            if (detallePresupuesto != null) {
    //                 await _context.DetallePresupuesto.AddAsync(detallePresupuesto);
    //             } else
    //             {
    //                 resultado.SetError("Error, el detalle de cobranza cargó nulo");
    //                 resultado.StatusCode = 500;
    //                 return Ok(resultado);
    //             }

    //         }


    //         var presupuestoT = new Presupuesto()
    //         {
    //             Id = comando.PresupuestoT.Id,
    //             Campania = comando.PresupuestoT.Campania,
    //             Total = comando.PresupuestoT.Total,
    //             Activo = comando.PresupuestoT.Activo,
    //             Aceptado = comando.PresupuestoT.Aceptado,
    //             Cliente = comando.PresupuestoT.Cliente,
    //             Numero = comando.PresupuestoT.Numero,
    //             DetallePresupuesto = new List<DetallePresupuesto>()
    //         };

    //         foreach (var detallePresupuestoT in comando.PresupuestoT.DetallePresupuesto)
    //         {
    //             var detallePresupuesto = new DetallePresupuesto()
    //             {
    //                 Id = detallePresupuestoT.Id,
    //                 CantidadProductos = detallePresupuestoT.CantidadProductos,
    //                 PrecioUnitario = detallePresupuestoT.PrecioUnitario,
    //                 Presupuesto = comando.PresupuestoT,
    //                 Producto = comando.ProductoT
    //             };

    //             if (detallePresupuesto != null) {
    //                 await _context.DetallePresupuesto.AddAsync(detallePresupuesto);
    //             } else
    //             {
    //                 resultado.SetError("Error, el detalle de cobranza cargó nulo");
    //                 resultado.StatusCode = 500;
    //                 return Ok(resultado);
    //             }
    //         }

    //             await _context.Producto.AddAsync(productoT);
    //             await _context.Presupuesto.AddAsync(presupuestoT);
    //             await _context.SaveChangesAsync();
    //         resultado.StatusCode = 200;
    //         return Ok(resultado);

    //     }
    //     catch (Exception ex)
    //     {

    //         resultado.SetError("Error al crear el presupuesto");
    //         resultado.StatusCode = 500;
    //         return Ok(resultado);
    //     }
    // }



    // [HttpPost("Crear")]
    // public async Task<ActionResult<ResultadoBase>> Crear([FromBody] ComandoPresupuesto comando)
    // {
    //     var cli = await _context.Cliente.
    //             Include(l => l.Localidad).
    //             Include(p => p.Localidad.Provincia).
    //             Include(v => v.CondicionIva).
    //             Where(c => c.Id.Equals(comando.ClienteId)).FirstOrDefaultAsync();
    //     var resultado = new ResultadoBase();
    //     try
    //     {
    //         var presupuesto = new Presupuesto()
    //         {
    //             Id = Guid.NewGuid(),
    //             Campania = comando.Campania,
    //             Total = comando.Total,
    //             Cliente = cli,
    //             Activo = comando.Activo,
    //             Aceptado = comando.Aceptado
    //         };

    //         if (presupuesto != null)
    //         {
    //             await _context.AddAsync(presupuesto);
    //             await _context.SaveChangesAsync();
    //             resultado.StatusCode = 200;
    //             return Ok(resultado);
    //         }
    //         else
    //         {
    //             resultado.SetError("Error al crear el presupuesto cargó nulo");
    //             resultado.StatusCode = 500;
    //             return Ok(resultado);
    //         }

    //     }
    //     catch (Exception ex)
    //     {

    //         resultado.SetError("Error al crear el presupuesto");
    //         resultado.StatusCode = 500;
    //         return Ok(resultado);
    //     }
    // }

    [HttpGet("ObtenerTodos")]
    public async Task<ActionResult<ResultadoPresupuesto>> ObtenerTodos()
    {
        try
        {
            var result = new ResultadoPresupuesto();
            var presupuestos = await _context.Presupuesto.
            Include(c => c.Cliente).
            Where(p => p.Activo).ToListAsync();


            if (presupuestos != null)
            {
                foreach (var p in presupuestos)
                {
                    var resAux = new ResultadoPresupuestoItem
                    {
                        Id = p.Id,
                        Numero = p.Numero,
                        Campania = p.campania,
                        Total = p.Total,
                        Cliente = p.Cliente,
                        Aceptado = p.Aceptado
                    };
                    result.listaPresupuestos.Add(resAux);
                    result.StatusCode = 200;
                }
                return Ok(result);
            }
            else
            {
                return Ok(result);
            }
        }
        catch (Exception ex)
        {
            return BadRequest("Error al obtener los presupuestos desde la API");
        }
    }

    [HttpGet("ObtenerCantidad")]
    public async Task<ActionResult<ResultadoPresupuesto>> ObtenerCantidad()
    {
        try
        {
            var result = new ResultadoPresupuesto();
            var presupuestos = await _context.Presupuesto.ToListAsync();
            int cantidad = presupuestos.Count();

            if (presupuestos != null)
            {
                return Ok(cantidad);
            }
            else
            {
                result.SetError("Error al obtener la cantidad de presupuestos");
                result.StatusCode = 400;
                return Ok(result);
            }
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
    }

    [HttpPost("ObtenerPresupuestosPorMes")]
    public async Task<ActionResult<ResultadoBase>> ObtenerPresupuestosPorMes([FromBody] ComandoPresupuestoMes comando)
    {

        try
        {
            var result = new ResultadoBase();
            var listadoFactMes = await _context.Presupuesto.
            OrderByDescending(n => n.Numero).
            Include(c => c.Cliente).
            Where(c => c.Fecha >= comando.fechaInicio && c.Fecha <= comando.fechaFin && c.Cliente.Id == comando.ClienteId).ToListAsync();
            if (listadoFactMes != null)
            {
                return Ok(listadoFactMes);
            }
            else
            {
                return Ok(result);
            }
        }
        catch (Exception ex)
        {
            return BadRequest("Error al obtener las facturas desde la API");
        }
    }

    [HttpGet("ObtenerCantidadAceptados")]
    public async Task<ActionResult<ResultadoPresupuesto>> ObtenerCantidadAceptados()
    {
        try
        {
            var result = new ResultadoPresupuesto();
            var presupuestos = await _context.Presupuesto.
            Where(p => p.Aceptado).ToListAsync();
            int cantidadAceptados = presupuestos.Count();

            if (presupuestos != null)
            {
                return Ok(cantidadAceptados);
            }
            else
            {
                result.SetError("Error al obtener la cantidad de presupuestos");
                result.StatusCode = 400;
                return Ok(result);
            }
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
    }

    [HttpPut("Eliminar")]
    public async Task<ActionResult<ResultadoPresupuesto>> Eliminar(Guid Id)
    {
        var resultado = new ResultadoBase();
        try
        {
            var presupuesto = await _context.Presupuesto.
            Include(c => c.Cliente).
            Where(p => p.Id.Equals(Id)).FirstOrDefaultAsync();
            if (presupuesto != null)
            {
                presupuesto.Activo = false;

                _context.Update(presupuesto);
                await _context.SaveChangesAsync();
                resultado.StatusCode = 200;
                return Ok(resultado);
            }
            else
            {
                resultado.SetError("Error al obtener el presupuesto");
                resultado.StatusCode = 400;
                return BadRequest(resultado);
            }

            if (Id.Equals(""))
            {
                resultado.SetError("No se puede eliminar dicho presupuesto");
                resultado.StatusCode = 400;
            }
        }
        catch (System.Exception)
        {

            resultado.SetError("Error al elimiar el presupuesto");
            resultado.StatusCode = 500;
            return NotFound(resultado);
        }

    }

    [HttpGet("ObtenerPdf")]
    public async Task<ActionResult<ResultadoPresupuestoPdf>> ObtenerPdf(Guid id)
    {
        try
        {
            var result = new ResultadoPresupuestoPdf();
            var presupuesto = await _context.Presupuesto.
            Where(p => p.Id.Equals(id)).FirstOrDefaultAsync();

            if (presupuesto != null)
            {
                result.Id = presupuesto.Id;
                result.Numero = presupuesto.Numero;
                result.Campania = presupuesto.campania;
                result.Total = presupuesto.Total;
                result.Aceptado = presupuesto.Aceptado;
                result.Cliente = presupuesto.Cliente;
                //presupuesto.p
                result.Items = new List<ResultadoItem>();
                var productos = await _context.Producto.
                Where(p => p.PresupuestoId == presupuesto.Id).ToListAsync();

                foreach (var producto in productos)
                {
                    ResultadoItem item = new ResultadoItem();
                    item.Producto = producto.nombreProducto;
                    item.Cantidad = producto.cantidadProductos;
                    item.Total = producto.cantidadProductos * producto.costoTotal;

                    result.Items.Add(item);
                }



                return Ok(result);
            }
            else
            {
                return Ok(result);
            }
        }
        catch (Exception ex)
        {
            return BadRequest("Error al obtener los presupuestos desde la API");
        }
    }
}