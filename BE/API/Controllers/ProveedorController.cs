using System.Data;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using Microsoft.EntityFrameworkCore;
using API.Results;
using API.Models;
using API.Commands;

namespace API.Controllers;
[Route("api/[controller]")]

public class ProveedorController : ControllerBase
{
    private readonly Context _context;

    public ProveedorController(Context context)
    {
        _context = context;
    }

    [HttpGet("ObtenerTodos")]
    public async Task<ActionResult<ResultadoProveedor>> ObtenerTodos()
    {
        try
        {
            var result = new ResultadoProveedor();
            var proveedores = await _context.Proveedor.
            Include(c => c.CondicionIva).
            Include(l => l.Localidad).
            Include(v => v.Localidad.Provincia).
            OrderBy(c => c.RazonSocial).
            Where(p => p.Activo).ToListAsync();
            if (proveedores != null)
            {
                foreach (var p in proveedores)
                {
                    var resAux = new ResultadoProveedorItem
                    {
                        Id = p.Id,
                        RazonSocial = p.RazonSocial,
                        Cuit = p.Cuit,
                        Calle = p.Calle,
                        Numero = p.Numero,
                        CodigoPostal = p.CodigoPostal,
                        Telefono = p.Telefono,
                        NombreContacto = p.NombreContacto,
                        Email = p.Email,
                        Comentario = p.Comentario,
                        Activo = p.Activo,
                        Localidad = p.Localidad,
                        CondicionIva = p.CondicionIva
                    };
                    result.listaProveedores.Add(resAux);
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
            return BadRequest("Error al obtener los proveedores desde la API");
        }
    }

    [HttpGet("ObtenerPorId")]
    public async Task<ActionResult<ResultadoProveedor>> ObtenerPorId(Guid id)
    {
        try
        {
            var result = new ResultadoProveedor();
            var proveedor = await _context.Proveedor.
            Include(v => v.CondicionIva).
            Include(l => l.Localidad).
            Include(p => p.Localidad.Provincia).
            Where(p => p.Id.Equals(id)).FirstOrDefaultAsync();
            if (proveedor != null)
            {
                return Ok(proveedor);
            }
            else
            {
                result.SetError("Error al obtener el proveedor");
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
    public async Task<ActionResult<ResultadoBase>> Crear([FromBody] ComandoProveedor comando)
    {
        var loc = await _context.Localidad.Where(l => l.Id.Equals(comando.LocalidadId)).FirstOrDefaultAsync();
        var cIva = await _context.CondicionIva.Where(c => c.Id.Equals(comando.CondicionIvaId)).FirstOrDefaultAsync();
        var resultado = new ResultadoBase();
        try
        {
            var proveedor = new Proveedor()
            {
                Id = Guid.NewGuid(),
                RazonSocial = comando.RazonSocial,
                Cuit = comando.Cuit,
                Calle = comando.Calle,
                Numero = comando.Numero,
                CodigoPostal = comando.CodigoPostal,
                Telefono = comando.Telefono,
                NombreContacto = comando.NombreContacto,
                Email = comando.Email,
                Comentario = comando.Comentario,
                Activo = true,
                CondicionIva = cIva,
                Localidad = loc
            };

            if (proveedor != null)
            {
                await _context.AddAsync(proveedor);
                await _context.SaveChangesAsync();
                resultado.StatusCode = 200;
                return Ok(comando);
            }
            else
            {
                resultado.SetError("Error al crear el proveedor cargó nulo");
                resultado.StatusCode = 500;
                return Ok(resultado);
            }

        }
        catch (Exception ex)
        {

            resultado.SetError("Error al crear el proveedor");
            resultado.StatusCode = 500;
            return Ok(resultado);
        }
    }

    [HttpPut("Actualizar")]
    public async Task<IActionResult> Actualizar([FromBody] ComandoProveedor comando)
    {
        var resultado = new ResultadoBase();
        try
        {
            if (comando.Id.Equals(""))
            {
                resultado.SetError("No se puede editar dicho proveedor");
                resultado.StatusCode = 400;
            }
            var proveedor = await _context.Proveedor.
            Include(v => v.CondicionIva).
            Include(l => l.Localidad).
            Include(p => p.Localidad.Provincia).
            Where(c => c.Id.Equals(comando.Id)).FirstOrDefaultAsync();
            if (proveedor != null)
            {

                var loc = await _context.Localidad.Where(l => l.Id.Equals(comando.LocalidadId)).FirstOrDefaultAsync();
                var cIva = await _context.CondicionIva.Where(v => v.Id.Equals(comando.CondicionIvaId)).FirstOrDefaultAsync();

                proveedor.Id = comando.Id;
                proveedor.RazonSocial = comando.RazonSocial;
                proveedor.Cuit = comando.Cuit;
                proveedor.Calle = comando.Calle;
                proveedor.Numero = comando.Numero;
                proveedor.CodigoPostal = comando.CodigoPostal;
                proveedor.Telefono = comando.Telefono;
                proveedor.NombreContacto = comando.NombreContacto;
                proveedor.Email = comando.Email;
                proveedor.Comentario = comando.Comentario;
                //proveedor.Activo =  comando.Activo;
                proveedor.Localidad = _context.Localidad.FirstOrDefault(l => l.Id == comando.LocalidadId); ;
                proveedor.CondicionIva = _context.CondicionIva.FirstOrDefault(c => c.Id == comando.CondicionIvaId);

                _context.Update(proveedor);
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

            resultado.SetError("Error al editar el proveedor");
            resultado.StatusCode = 500;
            return NotFound(resultado);
        }

    }

    [HttpPut("Eliminar")]
    public async Task<ActionResult<ResultadoProveedor>>Eliminar(Guid Id)
    {
        var resultado = new ResultadoBase();
        try
        {
            var proveedor = await _context.Proveedor.
            Include(l => l.Localidad).
            Include(i => i.CondicionIva).
            Where(i => i.Id.Equals(Id)).FirstOrDefaultAsync();
            if (proveedor != null)
            {
                proveedor.Activo = false;
                
                 _context.Update(proveedor);
                await _context.SaveChangesAsync();    
                resultado.StatusCode = 200;
                return Ok(resultado);
            }
            else
            {
                resultado.SetError("Error al obtener el proveedor");
                resultado.StatusCode = 400;
                return BadRequest(resultado);
            }
            
            if (Id.Equals(""))
            {
                resultado.SetError("No se puede eliminar dicho proveedor");
                resultado.StatusCode = 400;
            }
        }
        catch (System.Exception)
        {
            resultado.SetError("Error al elimiar el proveedor");
            resultado.StatusCode = 500;
            return NotFound(resultado);
        }
    }

}