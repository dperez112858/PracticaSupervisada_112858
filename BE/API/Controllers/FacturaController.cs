using System.Net.NetworkInformation;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using Microsoft.EntityFrameworkCore;
using API.Results;
using API.Models;
using API.Commands;
namespace API.Controllers;


[Route("api/[controller]")]

public class FacturaController : ControllerBase
{
    private readonly Context _context;
    public FacturaController(Context context)
    {
        _context = context;
    }

    [HttpPost("Crear")]
    public async Task<ActionResult<ResultadoBase>> Crear([FromBody] ComandoFactura comando)
    {
        var resultado = new ResultadoBase();
        var cliente = await _context.Cliente.
        Include(v => v.CondicionIva).
        Include(l => l.Localidad).
        Include(p => p.Localidad.Provincia).
        Where(c => c.Id.Equals(comando.ClienteId)).FirstOrDefaultAsync();
        try
        {
            if (cliente != null && comando.TipoCambio > 0)
            {
                var factura = new Factura()
                {
                    Id = Guid.NewGuid(),
                    Fecha = comando.Fecha,
                    Numero = comando.Numero,
                    TipoComprobante = comando.TipoComprobante,
                    Dolar = comando.Dolar,
                    TipoCambio = comando.TipoCambio,
                    NetoGravado = comando.NetoGravado * comando.TipoCambio,
                    Iva = comando.Iva * comando.TipoCambio,
                    Total = comando.Total * comando.TipoCambio,
                    Cancelada = false,
                    Cliente = cliente,
                    Saldo = comando.Total * comando.TipoCambio
                };
                ActualizarSaldoCliente(cliente);
                await _context.AddAsync(factura);
                await _context.SaveChangesAsync();
                ActualizarSaldoCliente(cliente);
                await _context.SaveChangesAsync();
                resultado.StatusCode = 200;
                return Ok(factura);
            }
            else
            {
                resultado.StatusCode = 400;
                resultado.SetError("Error al crear la factura, cliente o tipo de cambio inválido");
                return Ok(resultado);
            }

        }
        catch (Exception ex)
        {

            resultado.SetError("Error al crear la factura");
            resultado.StatusCode = 500;
            return Ok(resultado);
        }
    }

    [HttpGet("ObtenerAnterior")]
    public async Task<ActionResult<ResultadoFactura>> ObtenerAnterior()
    {
        try
        {
            var result = new ResultadoFactura();
            int anioAnterior = DateTime.Now.Year -1; // Obtener el año actual
            var facturas = await _context.Factura.
            OrderByDescending(n => n.Numero).
            Include(c => c.Cliente).
            Where(f => f.Cancelada == false && f.Fecha.Year == anioAnterior).ToListAsync();
            if (facturas != null)
            {
                foreach (var f in facturas)
                {
                    var resAux = new ResultadoFacturaItem
                    {
                        Id = f.Id,
                        Fecha = f.Fecha,
                        Numero = f.Numero,
                        TipoComprobante = f.TipoComprobante,
                        NetoGravado = f.NetoGravado,
                        Iva = f.Iva,
                        Dolar = f.Dolar,
                        TipoCambio = f.TipoCambio,
                        Total = f.Total,
                        Cancelada = f.Cancelada,
                        Cliente = f.Cliente,
                        Saldo = f.Saldo
                    };
                    result.listaFacturas.Add(resAux);
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
            return BadRequest("Error al obtener las facturas desde la API");
        }
    }
    [HttpGet("ObtenerTodas")]
    public async Task<ActionResult<ResultadoFactura>> ObtenerTodas()
    {
        try
        {
            var result = new ResultadoFactura();
            var facturas = await _context.Factura.
            OrderByDescending(n => n.Numero).
            Include(c => c.Cliente).
            Where(f => f.Cancelada == false).ToListAsync();
            if (facturas != null)
            {
                foreach (var f in facturas)
                {
                    var resAux = new ResultadoFacturaItem
                    {
                        Id = f.Id,
                        Fecha = f.Fecha,
                        Numero = f.Numero,
                        TipoComprobante = f.TipoComprobante,
                        NetoGravado = f.NetoGravado,
                        Iva = f.Iva,
                        Dolar = f.Dolar,
                        TipoCambio = f.TipoCambio,
                        Total = f.Total,
                        Cancelada = f.Cancelada,
                        Cliente = f.Cliente,
                        Saldo = f.Saldo
                    };
                    result.listaFacturas.Add(resAux);
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
            return BadRequest("Error al obtener las facturas desde la API");
        }
    }

    [HttpGet("ObtenerActual")]
    public async Task<ActionResult<ResultadoFactura>> ObtenerActual()
    {
        try
        {
            var result = new ResultadoFactura();
            int anioActual = DateTime.Now.Year; // Obtener el año actual
            var facturas = await _context.Factura.
            OrderByDescending(n => n.Numero).
            Include(c => c.Cliente).
            Where(f => f.Cancelada == false && f.Fecha.Year == anioActual).ToListAsync();
            if (facturas != null)
            {
                foreach (var f in facturas)
                {
                    var resAux = new ResultadoFacturaItem
                    {
                        Id = f.Id,
                        Fecha = f.Fecha,
                        Numero = f.Numero,
                        TipoComprobante = f.TipoComprobante,
                        NetoGravado = f.NetoGravado,
                        Iva = f.Iva,
                        Dolar = f.Dolar,
                        TipoCambio = f.TipoCambio,
                        Total = f.Total,
                        Cancelada = f.Cancelada,
                        Cliente = f.Cliente,
                        Saldo = f.Saldo
                    };
                    result.listaFacturas.Add(resAux);
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
            return BadRequest("Error al obtener las facturas desde la API");
        }
    }

    [HttpPost("ObtenerPedidosPorMes")]
    public async Task<ActionResult<ResultadoBase>> ObtenerPedidosPorMes([FromBody] ComandoFacturaMes comando)
    {
        try
        {
            var result = new ResultadoBase();
            var listadoFactMes = await _context.Factura.
            OrderByDescending(n => n.Numero).
            Include(c => c.Cliente).
            Where(f => f.Fecha >= comando.fechaInicio && f.Fecha <= comando.fechaFin && f.Cancelada == comando.cancelada).ToListAsync();
            if (listadoFactMes != null)
            {
                return Ok(listadoFactMes);
            }
            else
            {
                return Ok(result);
            }
        }
        catch (Exception ex)
        {
            return BadRequest("Error al obtener las facturas desde la API");
        }
    }

    [HttpGet("ObtenerPorCliente")]
    public async Task<ActionResult<ResultadoFactura>> ObtenerPorCliente(Guid id)
    {
        try
        {
            var result = new ResultadoFactura();
            var listaFacturas = await _context.Factura.
            Include(c => c.Cliente).
            Where(c => c.Cliente.Id.Equals(id) && c.Cancelada == true).ToListAsync();
            if (listaFacturas != null)
            {
                foreach (var f in listaFacturas)
                {
                    var resAux = new ResultadoFacturaItem
                    {
                        Id = f.Id,
                        Fecha = f.Fecha,
                        Numero = f.Numero,
                        TipoComprobante = f.TipoComprobante,
                        NetoGravado = f.NetoGravado,
                        Iva = f.Iva,
                        Dolar = f.Dolar,
                        TipoCambio = f.TipoCambio,
                        Total = f.Total,
                        Cancelada = f.Cancelada,
                        Cliente = f.Cliente,
                        Saldo = f.Saldo
                    };
                    result.listaFacturas.Add(resAux);
                    result.StatusCode = 200;
                }
                return Ok(result);
            }
            else
            {
                result.SetError("Error al obtener las facturas");
                result.StatusCode = 400;
                return Ok(result);
            }
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
    }

    [HttpGet("ObtenerPorId")]
    public async Task<ActionResult<ResultadoCliente>> ObtenerPorId(Guid id)
    {
        try
        {
            var result = new ResultadoFactura();
            var factura = await _context.Factura.
            Include(c => c.Cliente).
            Where(f => f.Id.Equals(id)).FirstOrDefaultAsync();
            if (factura != null)
            {
                return Ok(factura);
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

    [HttpPut("Actualizar")]
    public async Task<IActionResult> Actualizar([FromBody] ComandoFactura comando)
    {
        var resultado = new ResultadoBase();
        try
        {
            if (comando.Id.Equals(""))
            {
                resultado.SetError("No se puede editar dicha factura");
                resultado.StatusCode = 400;
            }
            var factura = await _context.Factura.
            Include(c => c.Cliente).
            Where(c => c.Id.Equals(comando.Id)).FirstOrDefaultAsync();
            if (factura != null)
            {
                var cli = await _context.Cliente.Where(p => p.Id.Equals(comando.ClienteId)).FirstOrDefaultAsync();

                factura.Id = comando.Id;
                factura.Total = comando.Total;
                factura.Fecha = comando.Fecha;
                factura.NetoGravado = comando.NetoGravado;
                factura.Iva = comando.Iva;
                factura.Numero = comando.Numero;
                factura.TipoComprobante = comando.TipoComprobante;
                factura.Cliente = cli;

                _context.Update(factura);
                await _context.SaveChangesAsync();
                ActualizarSaldoCliente(cli);
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


    [HttpPut("Eliminar")]
    public async Task<ActionResult<ResultadoFactura>> Eliminar(Guid Id)
    {
        var resultado = new ResultadoBase();
        try
        {
            var factura = await _context.Factura.
            Where(i => i.Id.Equals(Id)).FirstOrDefaultAsync();
            if (factura != null)
            {
                factura.Cancelada = true;
                
                _context.Update(factura);
                await _context.SaveChangesAsync();
                ActualizarSaldoCliente(factura.Cliente);
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

    private void ActualizarSaldoCliente(Cliente cliente)
    {
        double saldo = 0;
        double totalcobros = 0;
        var facturas = _context.Factura.Where(f => f.Cliente.Id == cliente.Id && f.Cancelada == false).ToList();
        var cobros = _context.Cobranza.Where(c => c.Cliente.Id == cliente.Id).ToList();
        
        if(facturas.Count > 0)
        {
            foreach(Factura factura in facturas)
            {
                saldo = saldo - factura.Total;
            }
        }
        if(cobros.Count > 0) 
        { 
            foreach(Cobranza cobranza in cobros)
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

    
    [HttpGet("ObtenerUltimoAnio")]
    public async Task<ActionResult<ResultadoFactura>> ObtenerUltimoAnio()
    {
        try
        {
            var fechaLimite = DateTime.Now.AddYears(-1);

            var result = new ResultadoFacturaUltimoAnio();
            var listaPorMes = await _context.Factura
            .Where(f => f.Fecha >= fechaLimite)
                .GroupBy(f => new { Mes = f.Fecha.Month, Anio = f.Fecha.Year })
                .Select(g => new
                {
                    Mes = g.Key.Mes,
                    Anio = g.Key.Anio,
                    TotalPorMes = g.Sum(f => f.Total)
                })
                .OrderByDescending(g => g.Anio)
                .ThenByDescending(g => g.Mes)
                .ToListAsync();

            if (listaPorMes != null)
            {
                foreach (var f in listaPorMes)
                {
                    var resAux = new ResultadoFacturaUltimoAnioItem
                    {
                        mes = f.Mes,
                        montoFacturadoMensual = f.TotalPorMes
                    };
                    result.ListadoFactuasUltimoAnio.Add(resAux);
                    result.StatusCode = 200;
                    result.SetError("Sin errores");    
                }
                return Ok(result);
            }
            else
            {
                result.SetError("Error al obtener las facturas");
                result.StatusCode = 400;
                return Ok(result);
            }
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
    }

    [HttpGet("ObtenerAnioActual")]
    public async Task<ActionResult<ResultadoFactura>> ObtenerAnioActual()
    {
        try
        {
            var fechaInicioAnio = new DateTime(DateTime.Now.Year, 1, 1);
            var result = new ResultadoFacturaUltimoAnio();
            var totalesPorMes = await _context.Factura
            .Where(f => f.Fecha >= fechaInicioAnio)
            .GroupBy(f => new { Mes = f.Fecha.Month, Anio = f.Fecha.Year })
            .Select(g => new
            {
                Mes = g.Key.Mes,
                Anio = g.Key.Anio,
                TotalPorMes = g.Sum(f => f.Total)
            })
            .OrderByDescending(g => g.Anio)
            .ThenByDescending(g => g.Mes)
            .ToListAsync();

            if (totalesPorMes != null)
            {
                foreach (var f in totalesPorMes)
                {
                    var resAux = new ResultadoFacturaUltimoAnioItem
                    {
                        mes = f.Mes,
                        montoFacturadoMensual = f.TotalPorMes
                    };
                    result.ListadoFactuasUltimoAnio.Add(resAux);
                    result.StatusCode = 200;
                    result.SetError("Sin errores");    
                }
                return Ok(result);
            }
            else
            {
                result.SetError("Error al obtener las facturas");
                result.StatusCode = 400;
                return Ok(result);
            }
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
    }

    [HttpGet("ObtenerAnioAnterior")]
    public async Task<ActionResult<ResultadoFactura>> ObtenerAnioAnterior()
    {
        try
        {
            var result = new ResultadoFacturaUltimoAnio();
            var fechaInicioAnioAnterior = new DateTime(DateTime.Now.Year - 1, 1, 1);
            var fechaFinAnioAnterior = new DateTime(DateTime.Now.Year - 1, 12, 31);

            var totalesPorMes = await _context.Factura
            .Where(f => f.Fecha >= fechaInicioAnioAnterior && f.Fecha <= fechaFinAnioAnterior)
            .GroupBy(f => new { Mes = f.Fecha.Month, Anio = f.Fecha.Year })
            .Select(g => new
            {
                Mes = g.Key.Mes,
                Anio = g.Key.Anio,
                TotalPorMes = g.Sum(f => f.Total)
            })
            .OrderByDescending(g => g.Anio)
            .ThenByDescending(g => g.Mes)
            .ToListAsync();
            if (totalesPorMes != null)
            {
                foreach (var f in totalesPorMes)
                {
                    var resAux = new ResultadoFacturaUltimoAnioItem
                    {
                        mes = f.Mes,
                        montoFacturadoMensual = f.TotalPorMes
                    };
                    result.ListadoFactuasUltimoAnio.Add(resAux);
                    result.StatusCode = 200;
                    result.SetError("Sin errores");    
                }
                return Ok(result);
            }
            else
            {
                result.SetError("Error al obtener las facturas");
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
