using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TheGuardians.Models;

namespace TheGuardians.DTOs
{
    public class PatrocinadorDTO
    {
        public int Monto { get; set; }
        [Required]
        [Column("origen_dinero")]
        [StringLength(100)]
        [Unicode(false)]

        public string Nombre { get; set; }
        public string OrigenDinero { get; set; }
        [Column("heroe_id")]
        public int HeroeId { get; set; }

        [ForeignKey("HeroeId")]
        [InverseProperty("Patrocinadors")]
        public virtual HeroeDTO Heroe { get; set; }
    }
}
