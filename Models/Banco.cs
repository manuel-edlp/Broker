﻿using Microsoft.EntityFrameworkCore;
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
        [ForeignKey("estado")]
        public int idBancoEstado { get; set; }

        public virtual BancoEstado estado { get; set; }

        [Required]
        public int numero { get; set; }

        
    }
}
