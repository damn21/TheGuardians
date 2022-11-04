using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheGuardians.DBContext;
using TheGuardians.DTOs;
using TheGuardians.Models;

namespace TheGuardians.Controllers
{
    [ApiController]
    [Route("api/heroes")]
    [Produces("application/json")]
    public class HeroesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public HeroesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpPost]
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
        public async Task<ActionResult> Agendar([FromBody] AgendumDTO agendumDTO)
        {
            var agenda = mapper.Map<Agendum>(agendumDTO);
            context.Add(agenda);
            await context.SaveChangesAsync();
            return Ok("Fecha registrada");
        }

        /// <summary>
        /// Obtiene todos los héroes registrados
        /// </summary>
        /// <returns></returns>
        /// 
        /// <response code="200">Retorna lista</response>
        /// <response code="400">Lista vacía</response>
        /// <response code="500">Error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<List<HeroeDTO>> GetHeroes()
        {
            var heroes = await context.Heroes.Include(x => x.IdPersonaNavigation)
                .Include(y => y.ContactoPersonals)
                .ToListAsync();
            return mapper.Map<List<HeroeDTO>>(heroes);
        }

        /// <summary>
        /// Obtiene un héroe especifico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        /// <response code="200">Retorna lista</response>
        /// <response code="400">Lista vacía</response>
        /// <response code="500">Error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{id:int}")]
        public async Task<List<HeroeDTO>> GetHeroes([FromRoute] int id)
        {
            var heroes = await context.Heroes.Include(x => x.IdPersonaNavigation)
                .Where(y => y.HeroeId.Equals(id))
                .ToListAsync();
            return mapper.Map<List<HeroeDTO>>(heroes);
        }

        /// <summary>
        /// Obtiene un héroe por nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        /// 
        /// <response code="200">Retorna lista</response>
        /// <response code="400">Lista vacía</response>
        /// <response code="500">Error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Obtiene un héroe por habilidad
        /// </summary>
        /// <param name="habilidad"></param>
        /// <returns></returns>
        /// 
        /// <response code="200">Retorna lista</response>
        /// <response code="400">Lista vacía</response>
        /// <response code="500">Error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Obtiene un héroe por habilidad
        /// </summary>
        /// <param name="contacto"></param>
        /// <returns></returns>
        /// 
        /// <response code="200">Retorna lista</response>
        /// <response code="400">Lista vacía</response>
        /// <response code="500">Lista vacía</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Obtiene lista de héroes adolescentes (menores de 21)
        /// </summary>
        /// <returns></returns>
        /// 
        /// <response code="200">Retorna lista</response>
        /// <response code="400">Lista vacía</response>
        /// <response code="500">Lista vacía</response>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [Route("edad/adolescentes")]
        public async Task<IActionResult> adolescentes()
        {
            var heroesA = await context.Heroes.Join(
                      context.Personas,
                      h => h.IdPersona,
                      p => p.Id,
                      (h, p) => new { h, p }
                      ).Where(k => k.p.Edad < 21)
                      .GroupBy(x => new
                      {
                          x.h.HeroeId,
                          x.p.Apodo,
                          x.p.Edad
                      })
                      .Select(y => new
                      {
                          y.Key.HeroeId,
                          y.Key.Apodo,
                          y.Key.Edad
                      }).OrderByDescending(z => z.Edad).ToListAsync();

            return Ok(heroesA);
        }

        /// <summary>
        /// Obtiene lista de héroes mayores
        /// </summary>
        /// <returns></returns>
        /// 
        /// <response code="200">Retorna lista</response>
        /// <response code="400">Lista vacía</response>
        /// <response code="500">Lista vacía</response>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [Route("edad/mayores")]
        public async Task<IActionResult> mayores()
        {
            var heroesA = await context.Heroes.Join(
                      context.Personas,
                      h => h.IdPersona,
                      p => p.Id,
                      (h, p) => new { h, p }
                      ).Where(k => k.p.Edad > 21)
                      .GroupBy(x => new
                      {
                          x.h.HeroeId,
                          x.p.Apodo,
                          x.p.Edad
                      })
                      .Select(y => new
                      {
                          y.Key.HeroeId,
                          y.Key.Apodo,
                          y.Key.Edad
                      }).OrderByDescending(z => z.Edad).ToListAsync();

            return Ok(heroesA);
        }

    }
}
