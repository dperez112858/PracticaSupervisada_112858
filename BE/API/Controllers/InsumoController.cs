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

public class InsumoController : ControllerBase
{
    private readonly Context _context;

    public InsumoController(Context context)
    {
        _context = context;
    }

    [HttpGet("ObtenerTodos")]
    public async Task<ActionResult<ResultadoInsumo>> ObtenerTodos()
    {
        try
        {
            var result = new ResultadoInsumo();
            var insumos = await _context.Insumo.
            Include(p => p.Proveedor).
            OrderBy(i => i.Descripcion).
            Where(i => i.Activo).ToListAsync();
            if (insumos != null)
            {
                foreach (var i in insumos)
                {
                    var resAux = new ResultadoInsumoItem
                    {
                        Id = i.Id,
                        Descripcion = i.Descripcion,
                        Precio = i.Precio,
                        FechaCompra = i.FechaCompra,
                        Proveedor = i.Proveedor,
                        Activo = i.Activo
                    };
                    result.listaInsumos.Add(resAux);
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
            return BadRequest("Error al obtener los insumos desde la API");
        }
    }


    // Obtener insumos por su Id
    [HttpGet("ObtenerPorId")]
    public async Task<ActionResult<ResultadoInsumo>> ObtenerPorId(Guid id)
    {
        try
        {
            var result = new ResultadoInsumo();
            var insumo = await _context.Insumo.
            Include(p => p.Proveedor).
            Where(i => i.Id.Equals(id)).FirstOrDefaultAsync();
            if (insumo != null)
            {
                return Ok(insumo);
            }
            else
            {
                result.SetError("Error al obtener el insumo");
                result.StatusCode = 400;
                return Ok(result);
            }
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
    }

    [HttpPost("Crear")]
    public async Task<ActionResult<ResultadoBase>> Crear([FromBody] ComandoInsumo comando)
    {
        var pro = await _context.Proveedor.
                Include(l => l.Localidad).
                Include(p => p.Localidad.Provincia).
                Include(v => v.CondicionIva).
                Where(l => l.Id.Equals(comando.ProveedorId)).FirstOrDefaultAsync();
        var resultado = new ResultadoBase();
        try
        {
            var insumo = new Insumo()
            {
                Id = Guid.NewGuid(),
                Descripcion = comando.Descripcion,
                Precio = comando.Precio,
                FechaCompra = comando.FechaCompra,
                Proveedor = pro,
                Activo = true
            };

            if (insumo != null)
            {
                await _context.AddAsync(insumo);
                await _context.SaveChangesAsync();
                resultado.StatusCode = 200;
                return Ok(comando);
            }
            else
            {
                resultado.SetError("Error al crear el insumo cargó nulo");
                resultado.StatusCode = 500;
                return Ok(resultado);
            }

        }
        catch (Exception ex)
        {

            resultado.SetError("Error al crear el insumo");
            resultado.StatusCode = 500;
            return Ok(resultado);
        }
    }

    [HttpPut("Eliminar")]
    public async Task<ActionResult<ResultadoInsumo>>Eliminar(Guid Id)
    {
        var resultado = new ResultadoBase();
        try
        {
            var insumo = await _context.Insumo.
            Include(p => p.Proveedor).
            Where(i => i.Id.Equals(Id)).FirstOrDefaultAsync();
            if (insumo != null)
            {
                insumo.Activo = false;
                
                 _context.Update(insumo);
                await _context.SaveChangesAsync();    
                resultado.StatusCode = 200;
                return Ok(resultado);
            }
            else
            {
                resultado.SetError("Error al obtener el insumo");
                resultado.StatusCode = 400;
                return BadRequest(resultado);
            }
            
            if (Id.Equals(""))
            {
                resultado.SetError("No se puede eliminar dicho insumo");
                resultado.StatusCode = 400;
            }
        }
        catch (System.Exception)
        {

            resultado.SetError("Error al elimiar el insumo");
            resultado.StatusCode = 500;
            return NotFound(resultado);
        }

    }

    [HttpPut("Actualizar")]
    public async Task<IActionResult> Actualizar([FromBody] ComandoInsumo comando)
    {
        var resultado = new ResultadoBase();
        try
        {
            if (comando.Id.Equals(""))
            {
                resultado.SetError("No se puede editar dicho insumo");
                resultado.StatusCode = 400;
            }
            var insumo = await _context.Insumo.
            Include(p => p.Proveedor).
            Where(c => c.Id.Equals(comando.Id)).FirstOrDefaultAsync();
            if (insumo != null)
            {

                var pro = await _context.Proveedor.Where(p => p.Id.Equals(comando.ProveedorId)).FirstOrDefaultAsync();

                insumo.Id = comando.Id;
                insumo.Descripcion = comando.Descripcion;
                insumo.Precio = comando.Precio;
                insumo.Proveedor = pro;
                

                _context.Update(insumo);
                await _context.SaveChangesAsync();
                resultado.StatusCode = 200;
            }
            else
            {
                resultado.SetError("No se encontro el id en la base de datos");
                resultado.StatusCode = 400;
                return BadRequest(resultado);
            }
            return Ok(comando);
        }
        catch (System.Exception)
        {

            resultado.SetError("Error al editar el insumo");
            resultado.StatusCode = 500;
            return NotFound(resultado);
        }

    }
}
