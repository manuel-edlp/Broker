using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Broker.Dtos
{
    public class TransaccionDtoAgregar
    {
        public DateTime fechaHora { get; set; }
        public String tipo { get; set; }
        public float monto { get; set; }
        public double cuentaOrigen { get; set; }
        public double cuentaDestino { get; set; }
    }
}
