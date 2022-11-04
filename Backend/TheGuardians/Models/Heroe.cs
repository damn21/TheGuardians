namespace TheGuardians.Models
{
    public partial class Heroe
    {
        public Heroe()
        {
            ContactoPersonals = new HashSet<ContactoPersonal>();
            Patrocinadors = new HashSet<Patrocinador>();
        }

        public int HeroeId { get; set; }
        public int IdPersona { get; set; }

        public virtual Persona IdPersonaNavigation { get; set; }
        public virtual Agendum Agendum { get; set; }
        public virtual ICollection<ContactoPersonal> ContactoPersonals { get; set; }
        public virtual ICollection<Patrocinador> Patrocinadors { get; set; }
    }
}
