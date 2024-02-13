using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Broker.Models
{
    public class Cuenta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public long numero { get; set; }

        [Required]
        public string cbu { get; set; }

        [Required]
        [ForeignKey("banco")]
        public int idBanco { get; set; }

        public virtual Banco banco { get; set; }


    }
}