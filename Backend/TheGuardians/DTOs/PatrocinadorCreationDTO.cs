using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheGuardians.DTOs
{
    public class PatrocinadorCreationDTO
    {
        public int Monto { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]

        public string Nombre { get; set; }
        public string OrigenDinero { get; set; }

        [ForeignKey("HeroeId")]
        public int HeroeId { get; set; }
    }
}
