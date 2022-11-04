using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TheGuardians.DTOs
{
    public class VillanoCreationDTO
    {
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string Origen { get; set; }
        public virtual PersonaDTO Persona { get; set; }
    }
}
