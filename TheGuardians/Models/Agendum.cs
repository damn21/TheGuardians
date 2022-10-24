using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheGuardians.Models
{
    public partial class Agendum
    {
        [Key]
        [Column("heroe_id")]
        public int HeroeId { get; set; }
        [Column("fecha", TypeName = "date")]
        public DateTime Fecha { get; set; }
        [Required]
        [Column("descripcion")]
        [StringLength(300)]
        [Unicode(false)]
        public string Descripcion { get; set; }

        [ForeignKey("HeroeId")]
        [InverseProperty("Agendum")]
        public virtual Heroe Heroe { get; set; }
    }
}
