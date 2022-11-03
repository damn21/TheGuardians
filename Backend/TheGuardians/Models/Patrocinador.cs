using System;
using System.Collections.Generic;

namespace TheGuardians.Models
{
    public partial class Patrocinador
    {
        public int PId { get; set; }
        public int Monto { get; set; }
        public string OrigenDinero { get; set; }
        public int HeroeId { get; set; }
        public string Nombre { get; set; }

        public virtual Heroe Heroe { get; set; }
    }
}
