using AutoMapper;
using Broker.Data;
using Broker.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Broker.Dtos;

namespace Broker.Services
{
    public class CuentaService
    {
        private readonly ApiDb _context;
        private readonly IMapper _mapper;


        public CuentaService(IConfiguration configuration, IMapper mapper, ApiDb context)
        {
            _context = context;

            _mapper = mapper;

        }

        public async Task<IEnumerable<Cuenta>> listarCuentas()
        {
            // Realiza una consulta a la base de datos para devolver todos las cuentas
            var cuentas = await _context.Cuenta.ToListAsync();

            // Devuelve la lista de cuentas
            return cuentas;
        }

        public async Task<int> agregarCuenta(long numero)
        {
            var cuenta = new Cuenta();
            cuenta.numero = numero;

            _context.Cuenta.Add(cuenta);

            await _context.SaveChangesAsync();

            return cuenta.cuentaId;
        }

        public async Task<CuentaDto> buscarCuenta(double numero)
        {
            // Realiza la búsqueda de transaccion por numero
            var cuenta = await _context.Cuenta

               .FirstOrDefaultAsync(c => c.numero == numero);

            var cuentaDto = _mapper.Map<CuentaDto>(cuenta);

            return cuentaDto;
        }
    }
}

