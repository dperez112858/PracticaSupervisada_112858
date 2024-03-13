using System.Data;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using Microsoft.EntityFrameworkCore;
using API.Results;
using API.Models;
using API.Commands;

namespace API.Controllers;
[Route("api/[controller]")]

public class ClienteController : ControllerBase
{
    private readonly Context _context;

    public ClienteController(Context context)
    {
        _context = context;
    }

    [HttpGet("ObtenerTodos")]
    public async Task<ActionResult<ResultadoCliente>> ObtenerTodos()
    {
        try
        {
            var result = new ResultadoCliente();
            var clientes = await _context.Cliente.
            Include(i => i.CondicionIva).
            Include(l => l.Localidad).
            Include(p => p.Localidad.Provincia).
            OrderBy(c => c.RazonSocial).
            Where(c => c.Activo).ToListAsync();
            if (clientes != null)
            {
                foreach (var c in clientes)
                {
                    var resAux = new ResultadoClienteItem
                    {
                        Id = c.Id,
                        RazonSocial = c.RazonSocial,
                        Cuit = c.Cuit,
                        Calle = c.Calle,
                        Numero = c.Numero,
                        CodigoPostal = c.CodigoPostal,
                        Telefono = c.Telefono,
                        NombreContacto = c.NombreContacto,
                        Email = c.Email,
                        Comentario = c.Comentario,
                        Activo = c.Activo,
                        Localidad = c.Localidad,
                        CondicionIva = c.CondicionIva,
                        Saldo = c.Saldo
                    };
                    result.listaClientes.Add(resAux);
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
            return BadRequest("Error al obtener los clientes desde la API");
        }
    }

    [HttpGet("ObtenerPorId")]
    public async Task<ActionResult<ResultadoCliente>> ObtenerPorId(Guid id)
    {
        try
        {
            var result = new ResultadoCliente();
            var cliente = await _context.Cliente.
            Include(v => v.CondicionIva).
            Include(l => l.Localidad).
            Include(p => p.Localidad.Provincia).
            Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
            if (cliente != null)
            {
                return Ok(cliente);
            }
            else
            {
                result.SetError("Error al obtener el cliente");
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
    public async Task<ActionResult<ResultadoBase>> Crear([FromBody] ComandoCliente comando)
    {
        var loc = await _context.Localidad.Where(l => l.Id.Equals(comando.LocalidadId)).FirstOrDefaultAsync();
        var cIva = await _context.CondicionIva.Where(c => c.Id.Equals(comando.CondicionIvaId)).FirstOrDefaultAsync();
        var resultado = new ResultadoBase();
        try
        {

            //dejo esto acá
            if (loc == null || cIva == null)
            {
                resultado.SetError("localidad o condición de IVA no encontrado.");
                resultado.StatusCode = 404;
                return NotFound(resultado);
            }
            //dejo esto acá
            var cliente = new Cliente()
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
                Localidad = loc,
                Saldo = 0
            };

            if (cliente != null)
            {
                await _context.AddAsync(cliente);
                await _context.SaveChangesAsync();
                resultado.StatusCode = 200;
                return Ok(comando);
            }
            else
            {
                resultado.SetError("Error al crear el cliente cargó nulo");
                resultado.StatusCode = 500;
                return Ok(resultado);
            }

        }
        catch (Exception ex)
        {

            resultado.SetError("Error al crear el cliente");
            resultado.StatusCode = 500;
            return Ok(resultado);
        }
    }

    [HttpPut("Actualizar")]
    public async Task<IActionResult> Actualizar([FromBody] ComandoCliente comando)
    {
        var resultado = new ResultadoBase();
        try
        {
            if (comando.Id.Equals(""))
            {
                resultado.SetError("No se puede editar dicho cliente");
                resultado.StatusCode = 400;
            }
            var cliente = await _context.Cliente.
            Include(v => v.CondicionIva).
            Include(l => l.Localidad).
            Include(p => p.Localidad.Provincia).
            Where(c => c.Id.Equals(comando.Id)).FirstOrDefaultAsync();
            if (cliente != null)
            {

                var loc = await _context.Localidad.Where(l => l.Id.Equals(comando.LocalidadId)).FirstOrDefaultAsync();
                var cIva = await _context.CondicionIva.Where(v => v.Id.Equals(comando.CondicionIvaId)).FirstOrDefaultAsync();

                cliente.Id = comando.Id;
                cliente.RazonSocial = comando.RazonSocial;
                cliente.Cuit = comando.Cuit;
                cliente.Calle = comando.Calle;
                cliente.Numero = comando.Numero;
                cliente.CodigoPostal = comando.CodigoPostal;
                cliente.Telefono = comando.Telefono;
                cliente.NombreContacto = comando.NombreContacto;
                cliente.Email = comando.Email;
                cliente.Comentario = comando.Comentario;
                cliente.Localidad = _context.Localidad.FirstOrDefault(l => l.Id == comando.LocalidadId); ;
                cliente.CondicionIva = _context.CondicionIva.FirstOrDefault(c => c.Id == comando.CondicionIvaId);

                _context.Update(cliente);
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
        catch (System.Exception ex)
        {

            resultado.SetError("Error al editar el cliente:" + ex.Message);
            resultado.StatusCode = 500;
            return NotFound(resultado);
        }

    }

    [HttpPut("Eliminar")]
    public async Task<ActionResult<ResultadoCliente>> Eliminar(Guid Id)
    {
        var resultado = new ResultadoBase();
        try
        {
            var cliente = await _context.Cliente.
            Include(l => l.Localidad).
            Include(i => i.CondicionIva).
            Where(i => i.Id.Equals(Id)).FirstOrDefaultAsync();
            if (cliente != null)
            {
                cliente.Activo = false;

                _context.Update(cliente);
                await _context.SaveChangesAsync();
                resultado.StatusCode = 200;
                return Ok(resultado);
            }
            else
            {
                resultado.SetError("Error al obtener el cliente");
                resultado.StatusCode = 400;
                return BadRequest(resultado);
            }

            if (Id.Equals(""))
            {
                resultado.SetError("No se puede eliminar dicho cliente");
                resultado.StatusCode = 400;
            }
        }
        catch (System.Exception)
        {

            resultado.SetError("Error al elimiar el cliente");
            resultado.StatusCode = 500;
            return NotFound(resultado);
        }

    }

}
