using System;
using System.Collections.Generic;

namespace TheGuardians.Models
{
    public partial class Agendum
    {
        public int HeroeId { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }

        public virtual Heroe Heroe { get; set; }
    }
}
