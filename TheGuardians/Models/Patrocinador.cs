using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheGuardians.Models
{
    [Table("Patrocinador")]
    public partial class Patrocinador
    {
        [Key]
        [Column("p_id")]
        public int PId { get; set; }
        [Column("monto", TypeName = "numeric(20, 13)")]
        public decimal Monto { get; set; }
        [Required]
        [Column("origen_dinero")]
        [StringLength(100)]
        [Unicode(false)]
        public string OrigenDinero { get; set; }
        [Column("heroe_id")]
        public int HeroeId { get; set; }

        [ForeignKey("HeroeId")]
        [InverseProperty("Patrocinadors")]
        public virtual Heroe Heroe { get; set; }
    }
}
