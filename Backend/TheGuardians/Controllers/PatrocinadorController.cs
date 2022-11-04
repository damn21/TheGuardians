using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheGuardians.DBContext;
using TheGuardians.DTOs;
using TheGuardians.Models;

namespace TheGuardians.Controllers
{
    [ApiController]
    [Route("api/patrocinador")]
    [Produces("application/json")]
    public class PatrocinadorController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PatrocinadorController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Agrega patrocinadores
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PatrocinadorCreationDTO patrocinadorCreationDTO)
        {
            var patrocinador = mapper.Map<Patrocinador>(patrocinadorCreationDTO);
            context.Add(patrocinador);
            await context.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Obtiene todos los patrocinadores registrados
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Retorna lista</response>
        /// <response code="400">Lista vacía</response>
        /// <response code="500">Error</response> 

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<List<PatrocinadorDTO>> GetAll()
        {
            var patrocinadores = await context.Patrocinadors.
                Include(x => x.Heroe.IdPersonaNavigation).ToListAsync();
            return mapper.Map<List<PatrocinadorDTO>>(patrocinadores);

        }

        /// <summary>
        /// Obtener patrocinador con mayor monto de patrocinio a un héroe especifico
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
        [Route("MayorMonto")]
        public async Task<IActionResult> MayorMonto(int id)
        {
            var patrocinadorM = await context.Heroes
                .Join(
                context.Patrocinadors,
                x => x.HeroeId,
                p => p.HeroeId,
                (x, p) => new { x, p }
                )
                .Join(
                context.Personas,
                y => y.x.IdPersona,
                ps => ps.Id, (y, ps) => new { y, ps }
                ).Where(k => k.y.x.HeroeId.Equals(id))
                .GroupBy(z => new { z.y.x.HeroeId, z.y.p.PId, z.y.p.Monto, z.y.p.OrigenDinero })
                .Select(rs => new
                {
                    rs.Key.HeroeId,
                    rs.Key.PId,
                    Monto = rs.Key.Monto,
                    rs.Key.OrigenDinero
                })
                .OrderByDescending(b => b.Monto).Take(1).ToArrayAsync();

            return Ok(patrocinadorM);
        }

    }
}
