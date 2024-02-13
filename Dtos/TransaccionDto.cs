using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Broker.Dtos
{
    public class TransaccionDto
    {
        public DateTime fechaHora { get; set; }
        public long numero { get; set; }
        public String tipo { get; set; }
        public String validacionEstado { get; set; }
        public String aceptadoEstado { get; set; }
        public float monto { get; set; }
        public string cbuOrigen { get; set; }
        public string cbuDestino { get; set; }



    }
}
