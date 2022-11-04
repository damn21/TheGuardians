using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheGuardians.DTOs
{
    public class VillanoDTO
    {
        [Key]
        public int VillanoId { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string Origen { get; set; }

        [ForeignKey("PersonaId")]
        public int PersonaId { get; set; }
        public virtual PersonaDTO Persona { get; set; }
    }
}
