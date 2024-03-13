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

public class ProductoController : ControllerBase
{
    private readonly Context _context;

    public ProductoController(Context context)
    {
        _context = context;
    }


    // [HttpPost("Crear")]
    // public async Task<ActionResult<ResultadoBase>> Crear([FromBody] ComandoProducto comando)
    // {
    //     var resultado = new ResultadoBase();
    //     try
    //     {
    //         var producto = new Producto()
    //         {
    //             Id = Guid.NewGuid(),
    //             FechaCreacion = DateTime.Now,
    //             Nombre = comando.Nombre,
    //             Banio = comando.Banio,
    //         };
    //             await _context.AddAsync(producto);
    //             await _context.SaveChangesAsync();
    //             resultado.StatusCode = 200;
    //             return Ok(producto);

    //     }
    //     catch (Exception ex)
    //     {

    //         resultado.SetError("Error al crear el producto");
    //         resultado.StatusCode = 500;
    //         return Ok(resultado);
    //     }
    // }

    // [HttpPut("Actualizar")]
    // public async Task<IActionResult> Actualizar([FromBody] ComandoProducto comando)
    // {
    //     var resultado = new ResultadoBase();
    //     try
    //     {
    //         if (comando.Id.Equals(""))
    //         {
    //             resultado.SetError("No se puede editar dicho producto");
    //             resultado.StatusCode = 400;
    //         }
    //         var producto = await _context.Producto.Where(c => c.Id.Equals(comando.Id)).FirstOrDefaultAsync();
    //         if (producto != null)
    //         {
    //             producto.Id = comando.Id;
    //             producto.Nombre = comando.Nombre;
    //             producto.Banio = comando.Banio;
    //             producto.CostoProducto = comando.CostoProducto;
    //             producto.Utilidad = comando.Utilidad;
    //             producto.CostoTotal = comando.CostoTotal;

    //             _context.Update(producto);
    //             await _context.SaveChangesAsync();
    //             resultado.StatusCode = 200;

    //         }
    //         else
    //         {
    //             resultado.SetError("No se encontro el id en la base de datos");
    //             resultado.StatusCode = 400;
    //             return BadRequest(resultado);
    //         }
    //         return Ok(comando);
    //     }
    //     catch (System.Exception)
    //     {

    //         resultado.SetError("Error al editar el producto");
    //         resultado.StatusCode = 500;
    //         return NotFound(resultado);
    //     }

    // }

    // [HttpGet("ObtenerTodos")]
    // public async Task<ActionResult<ResultadoProducto>> ObtenerTodos()
    // {
    //     try
    //     {
    //         var result = new ResultadoProducto();
    //         var productos = await _context.Producto.ToListAsync();
    //         if (productos != null)
    //         {
    //             foreach (var p in productos)
    //             {
    //                 var resAux = new ResultadoProductoItem
    //                 {
    //                     Id = p.Id,
    //                     Nombre = p.Nombre,
    //                     Banio = p.Banio,
    //                     FechaCreacion = p.FechaCreacion,
    //                     CostoProducto = p.CostoProducto,
    //                     Utilidad = p.Utilidad,
    //                     CostoTotal = p.CostoTotal
    //                 };
    //                 result.listaProductos.Add(resAux);
    //                 result.StatusCode = 200;
    //             }
    //             return Ok(result);
    //         }
    //         else
    //         {
    //             return Ok(result);
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         return BadRequest("Error al obtener los productos desde la API");
    //     }
    // }

    // [HttpGet("ActualizarCosto")]
    // public async Task<ActionResult<ResultadoProducto>> ActualizarCosto()
    // {
    //     try
    //     {
    //         var result = new ResultadoBase();

    //         var pro = await _context.Producto.
    //         OrderByDescending(p => p.FechaCreacion).FirstOrDefaultAsync();
    //         if( pro != null){
    //         var detalles = await _context.DetalleProducto.
    //         Where(d => d.Producto.Id.Equals(pro.Id)).ToListAsync();

    //         double suma = 0;
    //         foreach (var d in detalles)
    //         {
    //             suma += d.Total;
    //         }

    //         pro.CostoProducto = suma;
    //         pro.Utilidad = suma;
    //         pro.CostoTotal = suma * 2;

    //         _context.Update(pro);
    //         await _context.SaveChangesAsync();
    //         result.StatusCode = 200;
    //         return Ok(result);
    //         } else{
    //             result.StatusCode = 400;
    //             result.SetError("Producto nulo");
    //             return Ok(result);
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         return BadRequest("Error al obtener los productos desde la API");
    //     }
    // }

    // [HttpGet("ObtenerUltimo")]
    // public async Task<ActionResult<ResultadoProducto>> ObtenerUltimo()
    // {
    //     try
    //     {
    //         var result = new ResultadoProducto();
    //         var producto = await _context.Producto.
    //         OrderByDescending(p => p.FechaCreacion).FirstOrDefaultAsync();
    //         if (producto != null)
    //         {
    //             return Ok(producto);
    //         }
    //         else
    //         {
    //             result.SetError("Error al obtener el producto");
    //             result.StatusCode = 400;
    //             return Ok(result);
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         return Ok(ex.Message);
    //     }
    // }

    [HttpGet("ObtenerCantidad")]
    public async Task<ActionResult<ResultadoProducto>> ObtenerCantidad()
    {
        try
        {
            var result = new ResultadoProducto();
            //var productos = await _context.Producto.ToListAsync();

            var productos = await _context.Producto
    .Where(p => _context.Presupuesto.Any(pr => pr.Id == p.PresupuestoId && pr.Activo))
    .ToListAsync();

            int cantidad = productos.Count();

            if (productos != null)
            {
                return Ok(cantidad);
            }
            else
            {
                result.SetError("Error al obtener la cantidad de productos");
                result.StatusCode = 400;
                return Ok(result);
            }
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
    }


    [HttpGet("ObtenerDetalles")]
    public async Task<ActionResult<ResultadoDetalleProducto>> ObtenerDetalles(Guid id)
    {
        try
        {
            var result = new ResultadoDetalleProducto();
            var detalles = await _context.DetalleProducto
                .Where(d => d.ProductoId == id)
                .ToListAsync();

            if (detalles != null && detalles.Any())
            {
                foreach (var detalle in detalles)
                {
                    var resAux = new ResultadoDetalleProductoItem
                    {
                        Id = detalle.Id,
                        Cantidad = detalle.Cantidad,
                        PrecioInsumo = detalle.precioInsumo,
                        FechaCreacion = detalle.FechaCreacion,
                        Total = detalle.total,
                        Insumo = await _context.Insumo.Where(i => i.Id == detalle.InsumoId).FirstOrDefaultAsync(),
                        ProductoId = detalle.ProductoId
                    };
                    result.listaDetalleProducto.Add(resAux);
                }
                result.StatusCode = 200;
            }
            else
            {
                result.StatusCode = 404; // Indicar que no se encontraron detalles para el producto
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest("Error al obtener los detalles desde la API: " + ex.Message);
        }
    }

}