using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheGuardians.Models
{
    [Table("Villano")]
    public partial class Villano
    {
        [Key]
        [Column("villano_id")]
        public int VillanoId { get; set; }
        [Column("persona_id")]
        public int PersonaId { get; set; }
        [Required]
        [Column("origen")]
        [StringLength(100)]
        [Unicode(false)]
        public string Origen { get; set; }

        [ForeignKey("PersonaId")]
        [InverseProperty("Villanos")]
        public virtual Persona Persona { get; set; }
    }
}
