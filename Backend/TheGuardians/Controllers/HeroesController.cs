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

        [HttpPost]
        [Route("Agenda")]
        public async Task<ActionResult> Agendar([FromBody] Agendum agendum)
        {
            context.Add(agendum);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<List<HeroeDTO>> GetHeroes()
        {
            var heroes = await context.Heroes.Include(x => x.IdPersonaNavigation)
                .Include(y => y.ContactoPersonals)
                .ToListAsync();
            return mapper.Map<List<HeroeDTO>>(heroes);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<List<HeroeDTO>> GetHeroes([FromRoute]int id)
        {
            var heroes = await context.Heroes.Include(x => x.IdPersonaNavigation)
                .Where(y => y.HeroeId.Equals(id))
                .ToListAsync();
            return mapper.Map<List<HeroeDTO>>(heroes);
        }

        [HttpGet]
        [Route("nombre")]
        public async Task<ActionResult<List<HeroeDTO>>> ObtenerHeroeNombre(string nombre)
        {
            var heroes = await context.Heroes.Include(x => x.IdPersonaNavigation)
                .Where(y => y.IdPersonaNavigation.Nombre.Contains(nombre)).ToListAsync();

            if (!heroes.Any())
            {
                return NotFound("Heroe no encontrado");
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
        public async Task<ActionResult<List<ContactoPersonalDTO>>> ObtenerHeroeContacto(string contacto)
        {

            var heroes = await context.ContactoPersonals
                //.Include(x => x.Heroe.ContactoPersonals)
                .Include(z => z.Heroe.IdPersonaNavigation)
                .Where(y => y.Nombre.Contains(contacto)).ToListAsync();


            if (heroes == null)
            {
                return NotFound();
            }

            return mapper.Map<List<ContactoPersonalDTO>>(heroes);
        }

        [HttpGet]
        [Route("edad/adolescentes")]
        public async Task<IActionResult> adolescentes()
        {
            var heroesA = await context.Heroes.Join(
                      context.Personas,
                      h => h.IdPersona,
                      p => p.Id,
                      (h, p) => new { h, p }
                      ).Where(k => k.p.Edad < 21)
                      .GroupBy(x => new {
                          x.h.HeroeId,
                          x.p.Apodo,
                          x.p.Edad
                      })
                      .Select(y => new {
                          y.Key.HeroeId,
                          y.Key.Apodo,
                          y.Key.Edad
                      }).OrderByDescending(z => z.Edad).ToListAsync();

            return Ok(heroesA);
        }

        [HttpGet]
        [Route("edad/mayores")]
        public async Task<IActionResult> mayores()
        {
            var heroesA = await context.Heroes.Join(
                      context.Personas,
                      h => h.IdPersona,
                      p => p.Id,
                      (h, p) => new { h, p }
                      ).Where(k => k.p.Edad > 21)
                      .GroupBy(x => new {
                          x.h.HeroeId,
                          x.p.Apodo,
                          x.p.Edad
                      })
                      .Select(y => new {
                          y.Key.HeroeId,
                          y.Key.Apodo,
                          y.Key.Edad
                      }).OrderByDescending(z => z.Edad).ToListAsync();

            return Ok(heroesA);
        }

    }
}
