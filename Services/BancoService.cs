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
        private readonly CuentaService _cuentaService; 

        public BancoService(IConfiguration configuration, IMapper mapper, ApiDb context, CuentaService cuentaService)
        {
            _context = context;

            _mapper = mapper;

            _cuentaService = cuentaService;

        }

        public async Task<IEnumerable<BancoDto>> listarBancos()
        {
            // Realiza una consulta a la base de datos para devolver todos los Bancos
            var bancos = await _context.Banco
                .Include(b => b.estado)
                .Include(b => b.cuenta)
                .ToListAsync();
            var bancosdto = _mapper.Map<IEnumerable<BancoDto>>(bancos);
            // Devuelve la lista de Bancos
            return bancosdto;
        }

        public async Task<bool> agregarBanco(BancoDtoAgregar bancodto)
        {
            
            try
            {
                if (bancodto == null)
                {
                    return false;
                }



                var banco = new Banco();

                banco.razonSocial = bancodto.razonSocial;

                // estado:  1 para inactivo, 2 para activo
                // guardo id del estado, en el banco
                banco.estadoId = 2;

                

                // envio request para crear cuenta, me retorna su id
                var numeroCuenta = bancodto.cuenta;

                var cuentaId = await _cuentaService.agregarCuenta(numeroCuenta);

          

                // guardo id del estado, en el banco
                 banco.cuentaId = cuentaId;

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
