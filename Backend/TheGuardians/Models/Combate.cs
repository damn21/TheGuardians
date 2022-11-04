namespace TheGuardians.Models
{
    public partial class Combate
    {
        public int HeroeId { get; set; }
        public int VillanoId { get; set; }
        public string Resultado { get; set; }

        public virtual Heroe Heroe { get; set; }
        public virtual Villano Villano { get; set; }
    }
}
