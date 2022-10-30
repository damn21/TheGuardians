using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TheGuardians.Models;

namespace TheGuardians.DTOs
{
    public class CombateDTO
    {
        public int HeroeId { get; set; }
        public int VillanoId { get; set; }

        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Resultado { get; set; }

        [ForeignKey("HeroeId")]
        public virtual HeroeDTO Heroe { get; set; }
        [ForeignKey("VillanoId")]
        public virtual VillanoDTO Villano { get; set; }

    }
}
