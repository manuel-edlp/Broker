using AutoMapper;
using Broker.Data;
using Broker.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Broker.Dtos;
using System;

namespace Broker.Services
{
    public class TransaccionService
    {
        private readonly ApiDb _context;
        private readonly IMapper _mapper;
        private readonly CuentaService _cuentaService;


        public TransaccionService(IConfiguration configuration, IMapper mapper, ApiDb context, CuentaService cuentaService)
        {
            _context = context;

            _mapper = mapper;

            _cuentaService = cuentaService;

        }

        public async Task<IEnumerable<Transaccion>> listarTransacciones()
        {
            // Realiza una consulta a la base de datos para devolver todos los Bancos
            var cuentas = await _context.Transaccion.ToListAsync();

            // Devuelve la lista de Bancos
            return cuentas;
        }

        public async Task<bool> agregarTransaccion(TransaccionDtoAgregar transaccionDto)
        {
            try
            {
                if (transaccionDto == null)
                {
                    return false;
                }

                // creo Transaccion
                var transaccion = _mapper.Map<Transaccion>(transaccionDto);  

;               // estados: 1.pendiente  2.aceptada 3.rechazada
                transaccion.estadoId = 1; // seteo estado pendiente por defoult

                //estrategia: recibir origen y destino, enviarselos al renaper para verificar.
                // Si recibo Okey persisto las cuentas y acepto transaccion.
                // En caso contrario no persisto las cuentas y rechazo transferencia.

                // cuentaOrigen = await _httpClient.GetAsync(apiUrl/origen);
                // cuentaDestino = await _httpClient.GetAsync(apiUrl/destino);

                // en caso de no estar validadas rechazo
                if (cuentaOrigen == null || cuentaDestino == null)
                {
                    transaccion.estadoId = 3;  // cambio estado a rechazada
                    return false;
                }
                else // si estan validadas me fijo si ya las tengo creadas las cuentas, y si no las creo
                {
                    var origen = transaccion.cuentaOrigen.numero;
                    var destino = transaccion.cuentaDestino.numero;

                    var cuentaOrigen = await _cuentaService.buscarCuenta(origen);
                    var cuentaDestino = await _cuentaService.buscarCuenta(destino);

                    if(cuentaOrigen == null) // si no las tengo registradas las registro
                    {
                        await _cuentaService.agregarCuenta(cuentaOrigen.numero);
                    }
                    if (cuentaDestino == null)
                    {
                        await _cuentaService.agregarCuenta(cuentaDestino.numero);
                    }

                    // guardo id de la cuenta origen, en la transaccion
                    transaccion.cuentaOrigenId = cuentaOrigen.id;
                    transaccion.cuentaDestinoId = cuentaDestino.id;
                    transaccion.estadoId = 3; // seteo estado aceptada


                    // Agregar la transaccion al  contexto de la base de datos
                    _context.Transaccion.Add(transaccion);

                    // Guardar los cambios en la base de datos
                    await _context.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}


