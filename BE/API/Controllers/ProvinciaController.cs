using System.Linq;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using Microsoft.EntityFrameworkCore;
using API.Results;
using API.Models;
using API.Commands;
namespace API.Controllers;

[Route("api/[controller]")]

public class ProvinciaController : ControllerBase
{
    private readonly Context _context;
    public ProvinciaController(Context context)
    {
        _context = context;
    }

    [HttpGet("ObtenerTodas")]
    public async Task<ActionResult<List<Provincia>>> GetProvincias()
    {
        var resultado = await _context.Provincia.
        OrderBy(p => p.Nombre).ToListAsync();
        return Ok(resultado);
    }

}
