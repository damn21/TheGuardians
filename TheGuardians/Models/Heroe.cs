using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheGuardians.Models
{
    [Table("Heroe")]
    public partial class Heroe
    {
        public Heroe()
        {
            ContactoPersonals = new HashSet<ContactoPersonal>();
            Patrocinadors = new HashSet<Patrocinador>();
        }

        [Key]
        [Column("heroe_id")]
        public int HeroeId { get; set; }
        [Column("id_persona")]
        public int IdPersona { get; set; }

        [ForeignKey("IdPersona")]
        [InverseProperty("Heroes")]
        public virtual Persona IdPersonaNavigation { get; set; }
        [InverseProperty("Heroe")]
        public virtual Agendum Agendum { get; set; }
        [InverseProperty("Heroe")]
        public virtual ICollection<ContactoPersonal> ContactoPersonals { get; set; }
        [InverseProperty("Heroe")]
        public virtual ICollection<Patrocinador> Patrocinadors { get; set; }
    }
}
