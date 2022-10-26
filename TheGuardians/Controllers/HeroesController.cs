using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using TheGuardians.DBContext;
using TheGuardians.DTOs;
using TheGuardians.Models;

namespace TheGuardians.Controllers
{
    [ApiController]
    [Route("api/heroes")]
    public class HeroesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public HeroesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost("AgregarHeroe")]
        public async Task<ActionResult> AgregarHeroe([FromBody] HeroeCreationDTO heroeCreationDTO)
        {
            var existeHeroeConElMismoApodo = await context.Personas
                .AnyAsync(x => x.Apodo == heroeCreationDTO.IdPersonaNavigation.Apodo);

            if (existeHeroeConElMismoApodo)
            {
                return BadRequest($"Ya existe un heroe con el mismo apodo " +
                    $"{heroeCreationDTO.IdPersonaNavigation.Apodo}");
            }

            var heroe = mapper.Map<Heroe>(heroeCreationDTO);
            context.Add(heroe);
            await context.SaveChangesAsync();

            var heroeDTO = mapper.Map<HeroeDTO>(heroe);

            return new JsonResult(heroeDTO);

        }

        [HttpGet]
        public async Task<List<HeroeDTO>> GetHeroes()
        {
            var heroes = await context.Heroes.Include(x => x.IdPersonaNavigation).ToListAsync();
            return mapper.Map<List<HeroeDTO>>(heroes);
        }

        [HttpGet]
        [Route("nombre")]
        public async Task<ActionResult<List<HeroeDTO>>> ObtenerHeroeNombre(string nombre)
        {
            var heroes = await context.Heroes.Include(x => x.IdPersonaNavigation)
                .Where(y => y.IdPersonaNavigation.Nombre.Contains(nombre)).ToListAsync();

            if (heroes == null)
            {
                return NotFound();
            }

            return mapper.Map<List<HeroeDTO>>(heroes);
        }

        [HttpGet]
        [Route("habilidad")]
        public async Task<ActionResult<List<HeroeDTO>>> ObtenerHeroeHabilidad(string habilidad)
        {
            var heroes = await context.Heroes.Include(x => x.IdPersonaNavigation)
                .Where(y => y.IdPersonaNavigation.HabilidadPoder.Contains(habilidad)).ToListAsync();

            if (heroes == null)
            {
                return NotFound();
            }

            return mapper.Map<List<HeroeDTO>>(heroes);
        }

        [HttpGet]
        [Route("ContactoPersonal")]
        public async Task<ActionResult<ContactoPersonal>> ObtenerHeroeContacto(string contacto)
        {
            var contactoPersonal = await context.ContactoPersonals.Include(y => y.Heroe.IdPersonaNavigation)
                .FirstOrDefaultAsync(x => x.Nombre.Contains(contacto));

            if (contactoPersonal == null)
            {
                return NotFound();
            }

            return contactoPersonal;
        }

    }
}
