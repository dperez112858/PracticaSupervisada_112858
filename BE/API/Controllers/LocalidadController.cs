using System.Linq;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using Microsoft.EntityFrameworkCore;
using API.Results;
using API.Models;
using API.Commands;
namespace API.Controllers;

[Route("api/[controller]")]

public class LocalidadController : ControllerBase
{
    private readonly Context _context;
    public LocalidadController(Context context)
    {
        _context = context;
    }

[HttpPost("Crear")]
    public async Task<ActionResult<ResultadoBase>> Crear([FromBody] ComandoLocalidad comando)
    {
        var resultado = new ResultadoBase();
        var provincia = await _context.Provincia.Where(c => c.Id.Equals(comando.IdProvincia)).FirstOrDefaultAsync();
        try
        {
            var localidad = new Localidad()
            {
                Id = Guid.NewGuid(),
                Nombre = comando.Nombre,
                Provincia = provincia
            };
            if (localidad != null)
            {
                await _context.AddAsync(localidad);
                await _context.SaveChangesAsync();
                resultado.StatusCode = 200;
                return Ok(resultado);
            }
            else
            {
                resultado.SetError("Error al crear la localidad, cargó nulo");
                resultado.StatusCode = 500;
                return Ok(resultado);
            }
        }
        catch (Exception ex)
        {
            resultado.SetError("Error al crear la localidad: " + ex);
            resultado.StatusCode = 500;
            return Ok(resultado);
        }
    }
    
     [HttpGet("ObtenerTodas")]
    public async Task<ActionResult<List<Localidad>>> GetLocalidades()
    {
        var resultado = await _context.Localidad.
        Include(p => p.Provincia).ToListAsync();
        return Ok(resultado);
    }

    [HttpGet("ObtenerPorIdProvincia")]
    public async Task<ActionResult<ResultadoLocalidad>> ObtenerPorId(Guid id)
    {
        try
        {
            var result = new ResultadoLocalidad();
            var localidades = await _context.Localidad.
            Include(p => p.Provincia).
            OrderBy(l => l.Nombre).
            Where(p => p.Provincia.Id.Equals(id)).ToListAsync();
            if (localidades != null)
            {
                foreach (var l in localidades){
                    var resAux = new ResultadoLocalidadItem
                    {
                        Id = l.Id,
                        Nombre = l.Nombre,
                        Provincia = l.Provincia
                    };
                    result.listaLocalidades.Add(resAux);
                    result.StatusCode = 200;
                }
                
                return Ok(result);
            }
            else
            {
                result.SetError("Error al obtener la localidad");
                result.StatusCode = 400;
                return Ok(result);
            }
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
    }
}
