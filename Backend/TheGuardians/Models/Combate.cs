using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheGuardians.Models
{
    [Keyless]
    [Table("Combate")]
    public partial class Combate
    {
        [Column("heroe_id")]
        public int HeroeId { get; set; }
        [Column("villano_id")]
        public int VillanoId { get; set; }
        [Required]
        [Column("resultado")]
        [StringLength(50)]
        [Unicode(false)]
        public string Resultado { get; set; }

        [ForeignKey("HeroeId")]
        public virtual Heroe Heroe { get; set; }
        [ForeignKey("VillanoId")]
        public virtual Villano Villano { get; set; }
    }
}
