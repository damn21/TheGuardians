using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheGuardians.Models
{
    [Table("Persona")]
    public partial class Persona
    {
        public Persona()
        {
            Heroes = new HashSet<Heroe>();
            Villanos = new HashSet<Villano>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("nombre")]
        [StringLength(50)]
        [Unicode(false)]
        public string Nombre { get; set; }
        [Required]
        [Column("apellido")]
        [StringLength(50)]
        [Unicode(false)]
        public string Apellido { get; set; }
        [Required]
        [Column("apodo")]
        [StringLength(50)]
        [Unicode(false)]
        public string Apodo { get; set; }
        [Column("edad")]
        public int? Edad { get; set; }
        [Required]
        [Column("debilidad")]
        [StringLength(50)]
        [Unicode(false)]
        public string Debilidad { get; set; }
        [Required]
        [Column("habilidad_poder")]
        [StringLength(50)]
        [Unicode(false)]
        public string HabilidadPoder { get; set; }
        [Column("pais")]
        [StringLength(50)]
        [Unicode(false)]
        public string Pais { get; set; }

        [InverseProperty("IdPersonaNavigation")]
        public virtual ICollection<Heroe> Heroes { get; set; }
        [InverseProperty("Persona")]
        public virtual ICollection<Villano> Villanos { get; set; }
    }
}
