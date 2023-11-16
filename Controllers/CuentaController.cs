using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Broker.Models;
using Broker.Services;

namespace Broker.Controllers
{
    [ApiController]
    [Route("WebApi/[controller]")]
    public class CuentaController : ControllerBase
    {
        private readonly CuentaService _cuentaService;
        public CuentaController(CuentaService cuentaService)
        {
            _cuentaService = cuentaService;
        }

        [HttpGet("listar")] // Listar cuentas
        public async Task<IEnumerable<Cuenta>> listarCuentas()
        {
            var resultados = await _cuentaService.listarCuentas();

            return resultados;
        }
    }
}
