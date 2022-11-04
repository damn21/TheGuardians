using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace TheGuardians.DTOs
{
    public class AgendumDTO
    {
        [Key]
        public int HeroeId { get; set; }
        public DateTime Fecha { get; set; }
        [Required]
        [StringLength(300)]
        [Unicode(false)]
        public string Descripcion { get; set; }
    }
}
