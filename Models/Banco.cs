using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Broker.Models
{
    public class Banco
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string razonSocial { get; set; }

        [Required]
        [ForeignKey("estadoBancoId")]
        public int estadoId { get; set; }

        public virtual EstadoBanco estado { get; set; }

        [Required]
        [ForeignKey("cuentaId")]
        public int cuentaId { get; set; }

        public virtual Cuenta cuenta { get; set; }
    }
}
