using System;
using System.Collections.Generic;

namespace TheGuardians.Models
{
    public partial class Villano
    {
        public int VillanoId { get; set; }
        public int PersonaId { get; set; }
        public string Origen { get; set; }

        public virtual Persona Persona { get; set; }
    }
}
