using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheGuardians.DTOs
{
    public class HeroeDTO
    {

        [Key]
        public int HeroeId { get; set; }

        [ForeignKey("IdPersona")]
        public int IdPersona { get; set; }

        public virtual PersonaDTO IdPersonaNavigation { get; set; }

        public virtual ICollection<ContactoPersonalDTO> ContactoPersonals { get; set; }


    }
}
