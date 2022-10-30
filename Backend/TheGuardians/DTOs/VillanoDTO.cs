using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TheGuardians.Models;

namespace TheGuardians.DTOs
{
    public class VillanoDTO
    {
        [Key]
        public int VillanoId { get; set; }

        [ForeignKey("PersonaId")]
        public int PersonaId { get; set; }
        public virtual PersonaDTO Persona { get; set; }
    }
}
