using System.Data;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using Microsoft.EntityFrameworkCore;
using API.Results;
using API.Models;
using API.Commands;

namespace API.Controllers;
[Route("api/[controller]")]

public class DetalleProductoController : ControllerBase
{
    private readonly Context _context;

    public DetalleProductoController(Context context)
    {
        _context = context;
    }


    [HttpPost("Crear")]
    public async Task<ActionResult<ResultadoBase>> Crear([FromBody] ComandoDetalleProducto comando)
    {
        var resultado = new ResultadoBase();

        var producto = await _context.Producto.
        OrderByDescending(p => p.FechaCreacion).FirstOrDefaultAsync();

        var insumo = await _context.Insumo.Where(c => c.Id.Equals(comando.InsumoId)).FirstOrDefaultAsync();

        try
        {
            var detalleProducto = new ResultadoDetalleProductoItem()
            {
                Id = Guid.NewGuid(),
                Cantidad = comando.Cantidad,
                PrecioInsumo = comando.PrecioInsumo,
                FechaCreacion = DateTime.Now,
                Total = (comando.Cantidad * comando.PrecioInsumo),
                Insumo = insumo,
                Producto = producto
            };

            if (detalleProducto != null)
            {
                await _context.AddAsync(detalleProducto);
                await _context.SaveChangesAsync();
                resultado.StatusCode = 200;
                return Ok(detalleProducto);
            }
            else
            {
                resultado.SetError("Error al crear el detalle del producto, cargó nulo");
                resultado.StatusCode = 500;
                return Ok(resultado);
            }


        }
        catch (Exception ex)
        {

            resultado.SetError("Error al crear el producto");
            resultado.StatusCode = 500;
            return Ok(resultado);
        }
    }

    // [HttpGet("ObtenerPorId")]
    // public async Task<ActionResult<ResultadoDetalleProducto>> ObtenerPorId(Guid id)
    // {
    //     try
    //     {
    //         var result = new ResultadoDetalleProducto();
    //         var detalleProducto = await _context.DetalleProducto.
    //         Include(i => i.Insumo).
    //         Include(p => p.Producto).
    //         Where(i => i.Id.Equals(id)).FirstOrDefaultAsync();
    //         if (detalleProducto != null)
    //         {
    //             return Ok(detalleProducto);
    //         }
    //         else
    //         {
    //             result.SetError("Error al obtener el detalle de producto");
    //             result.StatusCode = 400;
    //             return Ok(result);
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         return Ok(ex.Message);
    //     }
    // }

    // [HttpGet("ObtenerPorIdProducto")]
    // public async Task<ActionResult<ResultadoDetalleProducto>> ObtenerPorIdProducto(Guid id)
    // {
    //     try
    //     {
    //         var result = new ResultadoDetalleProducto();
    //         var detalleProducto = await _context.DetalleProducto.
    //         Include(i => i.Insumo).
    //         Include(p => p.Producto).
    //         Where(i => i.Producto.Id.Equals(id)).FirstOrDefaultAsync();
    //         if (detalleProducto != null)
    //         {
    //             return Ok(detalleProducto);
    //         }
    //         else
    //         {
    //             result.SetError("Error al obtener el detalle de producto");
    //             result.StatusCode = 400;
    //             return Ok(result);
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         return Ok(ex.Message);
    //     }
    // }

    // [HttpGet("ObtenerTodos")]
    // public async Task<ActionResult<List<ResultadoDetalleProducto>>> ObtenerTodos()
    // {
    //     try
    //     {
    //         var result = new ResultadoDetalleProducto();
    //         var detallesProducto = await _context.DetalleProducto.
    //         Include(p => p.Producto).
    //         Include(i => i.Insumo).ToListAsync();
    //         if (detallesProducto != null)
    //         {
    //             foreach (var d in detallesProducto)
    //             {
    //                 var resAux = new ResultadoDetalleProductoItem
    //                 {
    //                     Id = d.Id,
    //                     Cantidad = d.Cantidad,
    //                     FechaCreacion = d.FechaCreacion,
    //                     PrecioInsumo = d.PrecioInsumo,
    //                     Total = d.Total,
    //                     Insumo = d.Insumo,
    //                     Producto = d.Producto
    //                 };
    //                 result.listaDetalleProducto.Add(resAux);
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
    //         return BadRequest("Error al obtener los insumos desde la API");
    //     }
    // }

    // [HttpGet("ObtenerTodosUltimoProducto")]
    // public async Task<ActionResult<List<ResultadoDetalleProducto>>> ObtenerTodosUltimoProducto()
    // {
    //     try
    //     {
    //         var result = new ResultadoDetalleProducto();
    //         var pro = await _context.Producto.
    //         OrderByDescending(p => p.FechaCreacion).FirstOrDefaultAsync();
            
    //         var detallesProducto = await _context.DetalleProducto.
    //         Include(p => p.Producto).
    //         Include(i => i.Insumo).
    //         Where(d => d.Producto.Id.Equals(pro.Id)).ToListAsync();
            
    //         if (detallesProducto != null)
    //         {
    //             foreach (var d in detallesProducto)
    //             {
    //                 var resAux = new ResultadoDetalleProductoItem
    //                 {
    //                     Id = d.Id,
    //                     Cantidad = d.Cantidad,
    //                     FechaCreacion = d.FechaCreacion,
    //                     PrecioInsumo = d.PrecioInsumo,
    //                     Total = d.Total,
    //                     Insumo = d.Insumo,
    //                     Producto = d.Producto
    //                 };
    //                 result.listaDetalleProducto.Add(resAux);
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
    //         return BadRequest("Error al obtener los insumos desde la API");
    //     }
    // }


}