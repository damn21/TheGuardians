using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheGuardians.DBContext;
using TheGuardians.Models;

namespace TheGuardians.Controllers
{
    [ApiController]
    [Route("api/combate")]
    [Produces("application/json")]
    public class CombateController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CombateController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtiene todos los combates de los héroes
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Retorna lista</response>
        /// <response code="400">Lista vacía</response>
        /// <response code="500">Error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Combate>>> GetAll()
        {
            var combates = await context.Combates.Join(
               context.Heroes,
               c => c.HeroeId,
               h => h.HeroeId, (c, h) => new { c, h }
               )
                .Join(context.Villanos,
                x => x.c.VillanoId,
                v => v.VillanoId, (x, v) => new { x, v }
               )
                .Join(context.Personas,
                y => y.v.PersonaId,
                p => p.Id, (y, p) => new { y, p }
                )
                //.GroupBy(z => new { z.y.x.c.HeroeId, z.y.v.VillanoId, z.p.Apodo, z.y.x.c.Resultado })
                .Select(z => new
                {
                    z.y.x.c.HeroeId,
                    z.y.v.VillanoId,
                    ApodoH = z.y.x.c.Heroe.IdPersonaNavigation.Apodo,
                    Apodo = z.p.Apodo,
                    Resultado = z.y.x.c.Resultado
                }).ToListAsync();

            return Ok(combates);
        }

        /// <summary>
        /// Obtiene el top 3 de héroes con más victorias
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Retorna lista</response>
        /// <response code="400">Lista vacía</response>
        /// <response code="500">Error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("heroes/victorias")]
        public async Task<ActionResult<List<Combate>>> Mayor()
        {

            var heroesM = await context.Combates.Where(z => z.Resultado.Contains("Ganador"))
               .GroupBy(x => x.Heroe.IdPersonaNavigation.Apodo)
                       .Select(group => new
                       {
                           Heroe = group.Key,
                           Victorias = group.Count()
                       })

                       .OrderByDescending(y => y.Victorias).Take(3).ToListAsync();

            return Ok(heroesM);

        }

        /// <summary>
        /// Obtiene el villano que más veces ha sido derrotado por un héroe adolescente
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Retorna lista</response>
        /// <response code="400">Lista vacía</response>
        /// <response code="500">Error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("villano/derrota")]
        public async Task<ActionResult<List<Combate>>> MayorV()
        {

            var villano = await context.Combates
               //.Join(context.Villanos,
               //c1 => c1.VillanoId,
               //v => v.VillanoId, (c1,v) => new {c1,v}
               //)
               .Join(context.Heroes,
               c => c.HeroeId,
               h => h.HeroeId, (c, h) => new { c, h }
               )
               .Join(context.Personas,
               p => p.h.IdPersona,
               p1 => p1.Id, (p, p1) => new { p, p1 }
               )
               .Where(z => z.p.c.Resultado.Contains("Ganador"))
               .Where(p1 => p1.p1.Edad < 21)
               .GroupBy(x => new
               {
                   x.p.c.Villano.Persona.Apodo,
                   x.p.c.HeroeId,
                   x.p1.Edad

               })
                       .Select(y => new
                       {
                           Edad_Heroe = y.Key.Edad,
                           Nombre_Villano = y.Key.Apodo,
                           Nombre_Heroe = y.Key.HeroeId,
                           Count = y.Count()
                       })

                       .OrderByDescending(y => y.Count).Take(1).ToListAsync();

            return Ok(villano);

        }

        /// <summary>
        /// Obtiene el villano con el que más ha combatido un héroe selecionado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        /// <response code="200">Retorna lista</response>
        /// <response code="400">Lista vacía</response>
        /// <response code="500">Error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("heroes/peleas/{id:int}")]
        public async Task<ActionResult<List<Combate>>> Peleas([FromRoute] int id)
        {

            var heroesM = await context.Combates.Where(z => z.HeroeId.Equals(id))
                .GroupBy(x => new { x.HeroeId, x.VillanoId, x.Villano.Persona.Apodo })
                       .Select(y => new
                       {
                           y.Key.HeroeId,
                           y.Key.VillanoId,
                           y.Key.Apodo,
                           Count = y.Count()
                       })
                       .OrderByDescending(y => y.Count).Take(1).ToListAsync();

            return Ok(heroesM);

        }

    }
}
