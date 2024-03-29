﻿using System;
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
        public int id { get; set; }

        [Required]
        public DateTime fechaHora { get; set; }

        [Required]
        public long numero { get; set; }

        [Required]
        [ForeignKey("tipo")]
        public int idTipo { get; set; }
        public virtual Tipo tipo { get; set; }

        public float monto { get; set; }

        [Required]
        [ForeignKey("cuentaOrigen")]
        public int idCuentaOrigen { get; set; }
        public virtual Cuenta cuentaOrigen { get; set; }


        [Required]
        [ForeignKey("cuentaDestino")]
        public int idCuentaDestino { get; set; }
        public virtual Cuenta cuentaDestino { get; set; }

        [Required]
        [ForeignKey("aceptadoEstado")]
        public int idAceptadoEstado { get; set; }
        public virtual AceptadoEstado aceptadoEstado { get; set; }

        [Required]
        [ForeignKey("validacionEstado")]
        public int idValidacionEstado { get; set; }
        public virtual ValidacionEstado validacionEstado { get; set; }

    }
}
