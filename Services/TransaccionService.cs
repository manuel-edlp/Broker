using AutoMapper;
using Broker.Data;
using Broker.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace Broker.Services
{
    public class TransaccionService
    {
        private readonly ApiDb _context;
        private readonly IMapper _mapper;


        public TransaccionService(IConfiguration configuration, IMapper mapper, ApiDb context)
        {
            _context = context;

            _mapper = mapper;

        }

        public async Task<IEnumerable<Transaccion>> listarTransacciones()
        {
            // Realiza una consulta a la base de datos para devolver todos los Bancos
            var cuentas = await _context.Transaccion.ToListAsync();

            // Devuelve la lista de Bancos
            return cuentas;
        }
    }
}


