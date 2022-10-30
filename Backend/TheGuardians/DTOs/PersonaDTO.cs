using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TheGuardians.DTOs
{
    public class PersonaDTO
    {
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Apellido { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Apodo { get; set; }
        public int? Edad { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Debilidad { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string HabilidadPoder { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Pais { get; set; }

    }
}
