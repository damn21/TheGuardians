using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TheGuardians.Models;

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
