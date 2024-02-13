using AutoMapper;
using Broker.Data;
using Broker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Broker.Dtos;

namespace Broker.Services
{
    public class BancoService
    {
        private readonly ApiDb _context;
        private readonly IMapper _mapper;

        public BancoService(IConfiguration configuration, IMapper mapper, ApiDb context)
        {
            _context = context;

            _mapper = mapper;

        }

        public async Task<IEnumerable<BancoDto>> listarBancos()
        {
            // Realiza una consulta a la base de datos para devolver todos los Bancos
            var bancos = await _context.Banco
                .Include(b => b.estado)
                .ToListAsync();
            var bancosdto = _mapper.Map<IEnumerable<BancoDto>>(bancos);
            // Devuelve la lista de Bancos
            return bancosdto;
        }
        public async Task<IEnumerable<BancoDto>> buscarBanco(int numero)
        {
            // Realiza una consulta a la base de datos para buscar Banco por numero
            var banco = await _context.Banco
            .Where(b => b.numero == numero)
            .Include(b => b.estado)
            .FirstOrDefaultAsync();

            var bancoDto = _mapper.Map<IEnumerable<BancoDto>>(banco);
            
            return bancoDto; // Devuelvo el banco
        }
        public async Task<int> getIdBanco(int numero)
        {
            // Realiza una consulta a la base de datos para buscar Banco por numero
            var banco = await _context.Banco
            .Where(b => b.numero == numero)
            .FirstOrDefaultAsync();
            
            return banco.id; // Devuelvo el banco
        }
        public async Task<bool> agregarBanco(BancoDtoAgregar bancodto, string cbu)
        {
            try
            {
                if (bancodto == null)
                {
                    return false;
                }

                var banco = new Banco();
                banco.razonSocial = bancodto.razonSocial;
                banco.idBancoEstado = 2; // seteo el estado ( 1 para inactivo, 2 para activo)
                banco.numero = bancodto.numero;

                // Agregar el Banco al contexto de la base de datos
                _context.Banco.Add(banco);

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }
    }
}
