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

        // Falta agregar acá y en el controller Transaccion una funcion que liste movimientos de transacciones por fecha por banco
        // Hay pensar si las ponemos en Banco o en Transaccion.

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
                // verificamos nosotros que exista el banco
                // Si recibo Okey persisto las cuentas y acepto transaccion.
                // En caso contrario no persisto las cuentas y rechazo transferencia.

                // cuentaOrigen = await _httpClient.GetAsync(apiUrl/origen);
                // cuentaDestino = await _httpClient.GetAsync(apiUrl/destino);

                // en caso de no estar validadas rechazo
                var cuentaOrige=1;
                var cuentaDestin=1;
                if (cuentaOrige == null || cuentaDestin == null)
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
                    
                    // Falta validar que el banco exista en nuestra BD

                    // Falta validar con el Renaper los tituler de las cuentas. Haciendo una request al sistema del renaper
                    // donde le pasamos el dni de los titulares de cuenta origen y destino.

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


