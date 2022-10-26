using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TheGuardians.DTOs
{
    public class PatrocinadorCreationDTO
    {
        public int Monto { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string OrigenDinero { get; set; }

        [ForeignKey("HeroeId")]
        public int HeroeId { get; set; }
    }
}
