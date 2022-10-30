using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheGuardians.Models
{
    [Table("ContactoPersonal")]
    public partial class ContactoPersonal
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("nombre")]
        [StringLength(50)]
        [Unicode(false)]
        public string Nombre { get; set; }
        [Column("heroe_id")]
        public int HeroeId { get; set; }

        [ForeignKey("HeroeId")]
        [InverseProperty("ContactoPersonals")]
        public virtual Heroe Heroe { get; set; }
    }
}
