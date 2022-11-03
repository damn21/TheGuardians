using System;
using System.Collections.Generic;

namespace TheGuardians.Models
{
    public partial class ContactoPersonal
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int HeroeId { get; set; }
        public string TipoR { get; set; }

        public virtual Heroe Heroe { get; set; }
    }
}
