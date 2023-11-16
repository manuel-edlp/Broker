using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Broker.Models;
using Broker.Services;
using Broker.Dtos;

namespace Broker.Controllers
{
    [ApiController]
    [Route("WebApi/[controller]")]
    public class TransaccionController : ControllerBase
    {
        private readonly TransaccionService _transaccionService;
        public TransaccionController(TransaccionService transaccionService)
        {
            _transaccionService = transaccionService;
        }

        [HttpGet("listar")] // Listar transacciones
        public async Task<IEnumerable<Transaccion>> listarTransacciones()
        {
            var resultados = await _transaccionService.listarTransacciones();

            return resultados;
        }

        [HttpPost] // agrega transaccion
        public async Task<IActionResult> agregarTransaccion([FromBody] TransaccionDtoAgregar transaccion)
        {
            if (transaccion == null)
            {
                return BadRequest("Los datos de la transaccion no son válidos.");
            }

            if (await _transaccionService.agregarTransaccion(transaccion))
            {

                // Devuelvo una respuesta de éxito con el código de estado 201 (Created)
                return CreatedAtAction("listarTransacciones", transaccion);
            }
            else
            {
                // Retorno respuesta de fallo del servidor con el codigo 500
                return StatusCode(500, "Transaccion no creada, error interno del servidor.");
            }
        }
    }
}

