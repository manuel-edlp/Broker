using AutoMapper;
using Broker.Data;
using Broker.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Broker.Dtos;
using System;
using System.Linq;

namespace Broker.Services
{
    public class TransaccionService
    {
        private readonly ApiDb _context;
        private readonly IMapper _mapper;
        private readonly CuentaService _cuentaService;
        private readonly BancoService _bancoService;



        public TransaccionService(IConfiguration configuration, IMapper mapper, ApiDb context, CuentaService cuentaService, BancoService bancoService)
        {
            _context = context;

            _mapper = mapper;

            _cuentaService = cuentaService;

            _bancoService = bancoService;

        }

        public async Task<IEnumerable<Transaccion>> listarTransacciones()
        {
            // Realiza una consulta a la base de datos para devolver todas las transacciones
            var transacciones = await _context.Transaccion.ToListAsync();

            // Devuelve la lista de transacciones
            return transacciones;
        }

        public async Task<IEnumerable<Transaccion>> listarTransaccionesPorFecha()
        {
            // Realiza una consulta a la base de datos para devolver todas las transacciones
            var transacciones = await _context.Transaccion.ToListAsync();

            // ordena las transacciones por fecha
            var transaccionesOrdenadas = transacciones.OrderBy(t => t.fechaHora);

            // Devuelve la lista de transacciones por fecha
            return transaccionesOrdenadas;
        }

        public async Task<bool> validarTransaccion(TransaccionDtoAgregar transaccionDto,int dniOrigen,int dniDestino)
        {
            //estrategia: 
            // -recibir cbu Origen y destino, Obtener bancos de los cbu y verificar existencia en nuestra bd
            // -recibir dni origen y destino, enviarselos al renaper para verificar.
            // -retorno true si cumple ambas verificaciones, de lo contrario retorno false
            try
            {
                string numeroBancoOrigen = transaccionDto.cbuOrigen.Substring(0, 4);
                string numeroBancoDestino = transaccionDto.cbuOrigen.Substring(0, 4);

                // busco los bancos en la bd
                var bancoOrigen = await _bancoService.buscarBanco(int.Parse(numeroBancoOrigen));
                var bancoDestino = await _bancoService.buscarBanco(int.Parse(numeroBancoDestino));

                if (bancoOrigen == null || bancoDestino == null) // si no existen en mi bd rechazo la transaccion
                {
                    return false;
                }

                // consulto al endpoint del Renaper la validez de los Dni (falta insertar endpoint)
                bool esValidoDniOrigen = true;// await _httpClient.GetAsync(apiUrl/);
                bool esValidoDniDestino = true;//await _httpClient.GetAsync(apiUrl/);

                if (esValidoDniOrigen == false || esValidoDniDestino == false)
                {
                    return false;
                }
              
                return true;  // retorno true si las validaciones son exitosas
             
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> agregarTransaccion(TransaccionDtoAgregar transaccionDto,int dniOrigen,int dniDestino)
        {
            // Estrategia:
            // valido la información de la transacción
            // Si recibo true persisto las cuentas, acepto transaccion, y envio numero de transaccion al banco.
            // En caso contrario no persisto las cuentas y rechazo transferencia.
            // si estan validadas me fijo si ya las tengo creadas las cuentas, y si no las creo
            try
            {
                if (transaccionDto == null)
                {
                    return false;
                }

                var transaccion = new Transaccion();// creo Transaccion
             
                // validacionEstados: (1: validando bancos, 2: validando DNI, 3: validacion exitosa, 4: validacion rechazada)
                transaccion.idValidacionEstado = 1; // seteo estado validando bancos

                // problema: tenemos que cambiar el estado de la transacción dentro de la funcion validarTransaccion y no dentro de esta funcion
                // hay que pasar de alguna manera el objeto transacción a la funcion validar para poder cambiarle el estado a la transaccion y se pueda ver el proceso de que 
                // cosas se estan validando

                bool validacion= await validarTransaccion(transaccionDto, dniOrigen, dniDestino);

                if (validacion == true)
                {
                    transaccion.idValidacionEstado = 3; // seteo transaccion como aceptada

                    var cuentaOrigen = transaccionDto.cbuOrigen.Substring(11, 21);
                    var cuentaDestino = transaccionDto.cbuDestino.Substring(11, 21);

                    // busco las cuentas en la BD
                    var cuentaOrigenBd = await _cuentaService.buscarCuenta(int.Parse(cuentaOrigen));
                    var cuentaDestinoBd = await _cuentaService.buscarCuenta(int.Parse(cuentaDestino));

                    if (cuentaOrigen == null) // si no las tengo registradas las registro
                    {
                        await _cuentaService.agregarCuenta(int.Parse(cuentaOrigen),transaccionDto.cbuOrigen);
                        // guardo id de la cuenta origen, en la transaccion
                        transaccion.idCuentaOrigen = cuentaOrigenBd.id;
                    }
                    if (cuentaDestino == null)
                    {
                        await _cuentaService.agregarCuenta(int.Parse(cuentaDestino), transaccionDto.cbuDestino);
                        // guardo id de la cuenta destino, en la transaccion
                        transaccion.idCuentaDestino = cuentaDestinoBd.id;
                    }
                    
                    transaccion.monto = transaccionDto.monto;
                    transaccion.fechaHora = transaccionDto.fechaHora;
                    // falta agregar el tipo buscando el id  del tipo en la bd con el nombre del tipo que nos pasan en el dto
                    
                    // Agrego la transaccion al  contexto de la base de datos
                    _context.Transaccion.Add(transaccion);

                    // Guardo los cambios en la base de datos
                    await _context.SaveChangesAsync();

                    // envio numero de transacción a banco (lo debo retornar en la funcion o lo envio directamente a un endpoint del banco ?)

                    return true;


                    
                }
                // si no cumple la validación seteo estado rechazada 
                transaccion.idValidacionEstado = 3;

                // Falta guardar la transaccion rechazada, acá solo le cambie el estado, pero hay que guardarle la info y agregarla a la bd
                return false;

                
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

