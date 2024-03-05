using System.Data;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using Microsoft.EntityFrameworkCore;
using API.Results;
using API.Models;
using API.Commands;

namespace API.Controllers;
[Route("api/[controller]")]

public class DetallePresupuestoController : ControllerBase
{
    private readonly Context _context;

    public DetallePresupuestoController(Context context)
    {
        _context = context;
    }


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