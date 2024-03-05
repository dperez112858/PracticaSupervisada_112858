using System.Data;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using Microsoft.EntityFrameworkCore;
using API.Results;
using API.Models;
using API.Commands;

namespace API.Controllers;
[Route("api/[controller]")]

public class CobranzaController : ControllerBase
{
    private readonly Context _context;

    public CobranzaController(Context context)
    {
        _context = context;
    }

    [HttpPost("Crear")]
    public async Task<ActionResult<ResultadoBase>> Crear([FromBody] ComandoCobranza comando)
    {
        var resultado = new ResultadoBase();
        try
        {
            var cliente = await _context.Cliente.Where(c => c.Id.Equals(comando.ClienteId)).FirstOrDefaultAsync();
            var cobranza = new Cobranza()
            {
                Id = Guid.NewGuid(),
                FechaLiquidacion = comando.FechaLiquidacion,
                Total = comando.Total,
                Cliente = cliente,
                // DetalleCobranza = comando.detalleCobranza
            };
            foreach (var detalleCobranza in comando.detalleCobranza)
            {
                var detalle = new DetalleCobranza()
                {
                    Id = Guid.NewGuid(),
                    ImporteCobrado = detalleCobranza.ImporteCobrado,
                    ImporteRetenido = detalleCobranza.ImporteRetenido,
                    ImporteTotal = detalleCobranza.ImporteTotal,
                    //FacturaId = "",
                    FechaPago = detalleCobranza.FechaPago,
                    TipoPagoId = detalleCobranza.TipoPagoId,
                    CobranzaId = cobranza.Id
                };
                await _context.DetalleCobranza.AddAsync(detalle);

                /*
                var factura = await _context.Factura.Where(f => f.Id.Equals(detalleCobranza.FacturaId)).FirstOrDefaultAsync();
                if (factura != null)
                {
                    if (factura.Saldo >= detalleCobranza.ImporteTotal)
                    {
                        factura.Saldo = factura.Total - detalleCobranza.ImporteTotal;
                    }
                    else if (factura.Saldo == 0)
                    {
                        factura.Cancelada = true;
                    }
                    else
                    {
                        resultado.SetError("El valor de la cobranza supera el saldo de la factura");
                        resultado.StatusCode = 500;
                        return Ok(resultado);
                    }
                    _context.Factura.Update(factura);
                }
                else
                {
                    resultado.SetError("Error al actualizar el saldo de la factura");
                    resultado.StatusCode = 500;
                }
                */
            }
            await _context.AddAsync(cobranza);
            await _context.SaveChangesAsync();
            ActualizarSaldoCliente(cliente);
            await _context.SaveChangesAsync();
            resultado.StatusCode = 200;
            return Ok(cobranza);
        }
        catch (Exception ex)
        {

            resultado.SetError("Error al crear la cobranza" + ex);
            resultado.StatusCode = 500;
            return Ok(resultado);
        }
    }

    [HttpGet("ObtenerPorId")]
    public async Task<ActionResult<ResultadoCobranza>> ObtenerPorId(Guid id)
    {
        try
        {
            var result = new ResultadoCobranza();
            var cobranza = await _context.Cobranza.
            Include(c => c.Cliente).
            Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
            if (cobranza != null)
            {
                result.data.Id = cobranza.Id;
                result.data.Cliente = cobranza.Cliente;
                result.data.FechaLiquidacion = cobranza.FechaLiquidacion;
                result.data.Total = cobranza.Total;
                result.data.DetalleCobranza = _context.DetalleCobranza.Where(dt => dt.CobranzaId== id).ToList();
                return Ok(result.data);
            }
            else
            {
                result.SetError("Error al obtener la cobranza");
                result.StatusCode = 400;
                return Ok(result);
            }
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
    }

    [HttpGet("ObtenerTodas")]
    public async Task<ActionResult<ResultadoCobranza>> ObtenerTodas()
    {
        try
        {
            var result = new ResultadoCobranza();
            var cobranzas = await _context.Cobranza.
            Include(c => c.Cliente).
            OrderBy(z => z.FechaLiquidacion).ToListAsync();

            if (cobranzas != null)
            {
                foreach (var c in cobranzas)
                {
                    var resAux = new ResultadoCobranzaItem
                    {
                        Id = c.Id,
                        FechaLiquidacion = c.FechaLiquidacion,
                        Cliente = c.Cliente,
                        Total = c.Total
                    };
                    result.listaCobranzas.Add(resAux);
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
            return BadRequest("Error al obtener las cobranzas desde la API");
        }
    }

    [HttpPost("ObtenerCobranzasPorMes")]
    public async Task<ActionResult<ResultadoBase>> ObtenerCobranzasPorMes([FromBody] ComandoCobranzaMes comando)
    {
        try
        {
            var result = new ResultadoBase();
            var listadoCobranzasMes = await _context.Cobranza.
            Include(c => c.Cliente).
            Where(f => f.FechaLiquidacion >= comando.fechaInicio && f.FechaLiquidacion <= comando.fechaFin).ToListAsync();
            if (listadoCobranzasMes != null)
            {
                return Ok(listadoCobranzasMes);
            }
            else
            {
                return Ok(result);
            }
        }
        catch (Exception ex)
        {
            return BadRequest("Error al obtener las cobranzas desde la API");
        }
    }


    [HttpPut("Eliminar")]
    public async Task<ActionResult<ResultadoCobranza>> Eliminar(Guid Id)
    {
        var resultado = new ResultadoBase();
        try
        {
            var cobranza = await _context.Cobranza.
            Where(i => i.Id.Equals(Id)).FirstOrDefaultAsync();
            if (cobranza != null)
            {
                var cliente = cobranza.Cliente;
                foreach (DetalleCobranza detcob in _context.DetalleCobranza.Where(dt => dt.CobranzaId == Id))
                {
                    _context.Remove(detcob);
                }

                _context.Remove(cobranza);
                await _context.SaveChangesAsync();
                ActualizarSaldoCliente(cliente);
                await _context.SaveChangesAsync();

                resultado.StatusCode = 200;
                return Ok(resultado);
            }
            else
            {
                resultado.SetError("Error al obtener la factura");
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
    public async Task<IActionResult> Actualizar([FromBody] ComandoCobranza comando)
    {
        var resultado = new ResultadoBase();
        try
        {

            var cobranza = await _context.Cobranza.
            Include(c => c.Cliente).
            Where(c => c.Id.Equals(comando.Id)).FirstOrDefaultAsync();
            if (cobranza != null)
            {
                var cliente = await _context.Cliente.Where(c => c.Id.Equals(comando.ClienteId)).FirstOrDefaultAsync();
                cobranza.FechaLiquidacion = comando.FechaLiquidacion;
                cobranza.Total = comando.Total;
                cobranza.Cliente = cliente;

                _context.Update(cobranza);
                await _context.SaveChangesAsync();


                foreach (DetalleCobranza detcob in _context.DetalleCobranza.Where(dt => dt.CobranzaId == comando.Id))
                {
                    _context.Remove(detcob);
                }
                foreach (var detalleCobranza in comando.detalleCobranza)
                {
                    var detalle = new DetalleCobranza()
                    {
                        Id = Guid.NewGuid(),
                        ImporteCobrado = detalleCobranza.ImporteCobrado,
                        ImporteRetenido = detalleCobranza.ImporteRetenido,
                        ImporteTotal = detalleCobranza.ImporteTotal,
                        //FacturaId = detalleCobranza.FacturaId,
                        FechaPago = detalleCobranza.FechaPago,
                        TipoPagoId = detalleCobranza.TipoPagoId,
                        CobranzaId = cobranza.Id
                    };
                    await _context.DetalleCobranza.AddAsync(detalle);
                }
                ActualizarSaldoCliente(cliente);
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

            resultado.SetError("Error al editar la factura");
            resultado.StatusCode = 500;
            return NotFound(resultado);
        }

    }

    private void ActualizarSaldoCliente(Cliente cliente)
    {
        double saldo = 0;
        double totalcobros = 0;
        var facturas = _context.Factura.Where(f => f.Cliente.Id == cliente.Id && f.Cancelada == false).ToList();
        var cobros = _context.Cobranza.Where(c => c.Cliente.Id == cliente.Id).ToList();

        if (facturas.Count > 0)
        {
            foreach (Factura factura in facturas)
            {
                saldo = saldo - factura.Total;
            }
        }
        if (cobros.Count > 0)
        {
            foreach (Cobranza cobranza in cobros)
            {
                saldo = saldo + cobranza.Total;
                totalcobros = totalcobros + cobranza.Total;
            }
        }
        cliente.Saldo = Convert.ToDecimal(saldo);

        foreach (Factura factura in facturas.OrderBy(f => f.Fecha))
        {
            factura.Saldo = factura.Total;
            if (totalcobros > factura.Total)
            {
                factura.Saldo = 0;
                totalcobros = totalcobros - factura.Total;
            }
            else
            {
                factura.Saldo = factura.Saldo - totalcobros;
                totalcobros = 0;
            }
            _context.Factura.Update(factura);
        }
        _context.Cliente.Update(cliente);
    }
}