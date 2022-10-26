using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TheGuardians.DTOs
{
    public class CombateCreationDTO
    {
        public int HeroeId { get; set; }
        public int VillanoId { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Resultado { get; set; }
    }
}
