namespace TheGuardians.DTOs
{
    public class HeroeCreationDTO
    {
        //[Key]
        //public int HeroeId { get; set; }

        //[ForeignKey("IdPersona")]
        //public int IdPersona { get; set; }

        public virtual PersonaDTO IdPersonaNavigation { get; set; }

    }
}
