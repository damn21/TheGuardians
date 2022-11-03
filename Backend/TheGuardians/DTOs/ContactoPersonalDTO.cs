using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TheGuardians.DTOs
{
    public class ContactoPersonalDTO
    {
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Nombre { get; set; }

        [ForeignKey("HeroeId")]
        public int HeroeId { get; set; }

        public string TipoR { get; set; }
        //public virtual HeroeDTO Heroe { get; set; }


    }
}
