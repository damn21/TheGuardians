using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TheGuardians.Models;
using Microsoft.EntityFrameworkCore;

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
