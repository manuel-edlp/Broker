using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Broker.Models
{
    public class Transaccion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime fechaHora { get; set; }

        [Required]
        [ForeignKey("tipoTransaccionId")]
        public int tipoId { get; set; }
        public virtual TipoTransaccion tipo { get; set; }

        [Required]
        [ForeignKey("estadoTransaccionId")]
        public int estadoId { get; set; }
        public virtual EstadoTransaccion estado { get; set; }

        public float monto { get; set; }

        [Required]
        [ForeignKey("cuentaId")]
        public int cuentaOrigenId { get; set; }
        public virtual Cuenta cuentaOrigen { get; set; }


        [Required]
        [ForeignKey("cuentaId")]
        public int cuentaDestinoId { get; set; }
        public virtual Cuenta cuentaDestino { get; set; }

    }
}
