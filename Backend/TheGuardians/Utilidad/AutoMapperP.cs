using AutoMapper;
using TheGuardians.DTOs;
using TheGuardians.Models;

namespace TheGuardians.Utilidad
{
    public class AutoMapperP : Profile
    {
        public AutoMapperP()
        {
            CreateMap<HeroeCreationDTO, Heroe>();
            CreateMap<PersonaDTO, Persona>().ReverseMap();
            CreateMap<Heroe, HeroeDTO>();
            CreateMap<PatrocinadorCreationDTO, Patrocinador>();
            CreateMap<Patrocinador, PatrocinadorDTO>();
            CreateMap<Combate, CombateDTO>();
            CreateMap<VillanoCreationDTO, Villano>();
            CreateMap<Villano, VillanoDTO>();
            CreateMap<ContactoPersonal, ContactoPersonalDTO>();
            CreateMap<AgendumDTO, Agendum>();
        }

    }
}
