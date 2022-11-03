using System;
using System.Collections.Generic;

namespace TheGuardians.Models
{
    public partial class Persona
    {
        public Persona()
        {
            Heroes = new HashSet<Heroe>();
            Villanos = new HashSet<Villano>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Apodo { get; set; }
        public int? Edad { get; set; }
        public string Debilidad { get; set; }
        public string HabilidadPoder { get; set; }
        public string Pais { get; set; }

        public virtual ICollection<Heroe> Heroes { get; set; }
        public virtual ICollection<Villano> Villanos { get; set; }
    }
}
