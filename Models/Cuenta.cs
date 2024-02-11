using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Broker.Models
{
    public class Cuenta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cuentaId { get; set; }

        [Required]
        public long numero { get; set; }

        [Required]
        public string cbu { get; set; }


    }
}