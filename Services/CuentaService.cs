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
        private readonly BancoService _bancoService;


        public CuentaService(IConfiguration configuration, IMapper mapper, ApiDb context, BancoService bancoService)
        {
            _context = context;

            _mapper = mapper;

            _bancoService = bancoService;

        }

        public async Task<IEnumerable<Cuenta>> listarCuentas()
        {
            // Realiza una consulta a la base de datos para devolver todos las cuentas
            var cuentas = await _context.Cuenta.ToListAsync();

            // Devuelve la lista de cuentas
            return cuentas;
        }

        public async Task<int> agregarCuenta(long numero, string cbu)
        {
            var bancoNumero = int.Parse(cbu.Substring(0, 4));  // extraigo del cbu el numero identificador del banco
            var idBanco = await _bancoService.getIdBanco(bancoNumero);  // busco el banco en mi bd y guardo el id

            var cuenta = new Cuenta();
            cuenta.numero = numero;
            cuenta.cbu = cbu;
            cuenta.idBanco = idBanco;  // engancho la FK con el id del banco que consegui

            _context.Cuenta.Add(cuenta);

            await _context.SaveChangesAsync();

            return cuenta.id;
        }

        public async Task<CuentaDto> buscarCuenta(long numero)
        {
            // Realiza la búsqueda de cuenta por numero
            var cuenta = await _context.Cuenta

               .FirstOrDefaultAsync(c => c.numero == numero);

            var cuentaDto = _mapper.Map<CuentaDto>(cuenta);

            return cuentaDto;
        }
    }
}

