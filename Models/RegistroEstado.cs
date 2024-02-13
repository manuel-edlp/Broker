using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Broker.Models
{
    public class RegistroEstado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public DateTime fechaHora { get; set; }

        [Required]
        [MaxLength(200)]
        public string descripcion { get; set; }

        [Required]
        [ForeignKey("aceptadoEstado")]
        public int idAceptadoEstado { get; set; }
        public virtual AceptadoEstado aceptadoEstado { get; set; }

        [Required]
        [ForeignKey("validacionEstado")]
        public int idValidacionEstado { get; set; }
        public virtual ValidacionEstado validacionEstado { get; set; }

        [Required]
        [ForeignKey("transaccion")]
        public int idTransaccion { get; set; }
        public virtual Transaccion transaccion { get; set; }
    }
}
