using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Route("api/[controller]")]
public class CondicionIvaController : ControllerBase
{
    private readonly Context _context;
    public CondicionIvaController(Context context)
    {
        _context = context;
    }

    [HttpGet("ObtenerTodas")]
    public async Task<ActionResult<List<CondicionIva>>> GetCondicionIva()
    {
        var resultado = await _context.CondicionIva.ToListAsync();
        return Ok(resultado);
    }

}
