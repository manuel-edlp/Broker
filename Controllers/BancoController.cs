using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Broker.Models;
using Microsoft.AspNetCore.Http;
using Broker.Services;

using System.Diagnostics;
using Broker.Dtos;

namespace Broker.Controllers
{
    [ApiController]
    [Route("WebApi/[controller]")]
    public class BancoController : ControllerBase
    {
        private readonly BancoService _bancoService;
        public BancoController(BancoService bancoService)
        {
            _bancoService = bancoService;
        }

        [HttpGet("listar")] // Listar Bancos
        public async Task<IEnumerable<BancoDto>>listarBancos()
        {
            var resultados = await _bancoService.listarBancos();

            return resultados;
        }

        [HttpPost] // agrega banco
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BancoDtoAgregar))]
        public async Task<IActionResult>agregarBanco([FromBody] BancoDtoAgregar banco, string cbu)
        {
            if (banco == null)
            {
                return BadRequest("Los datos del banco no son válidos.");
            }

            if (await _bancoService.agregarBanco(banco,cbu))
            {

                // Devuelvo una respuesta de éxito con el código de estado 201 (Created)
                return CreatedAtAction("listarBancos", banco);
            }
            else
            {
                // Retorno respuesta de fallo del servidor con el codigo 500
                return StatusCode(500, "Banco no creado, error interno del servidor.");
            }
        }

    }
}
