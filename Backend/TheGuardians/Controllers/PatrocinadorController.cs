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
    public class PatrocinadorController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PatrocinadorController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost("Agregar patrocinador")]
        public async Task<ActionResult> Post([FromBody] PatrocinadorCreationDTO patrocinadorCreationDTO)
        {
            var patrocinador = mapper.Map<Patrocinador>(patrocinadorCreationDTO);
            context.Add(patrocinador);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<List<PatrocinadorDTO>> GetAll()
        {
            var patrocinadores = await context.Patrocinadors.
                Include(x => x.Heroe.IdPersonaNavigation).ToListAsync();
            return mapper.Map<List<PatrocinadorDTO>>(patrocinadores);

        }

        [HttpGet]
        [Route("Mayor_Monto")]
        public async Task<IActionResult> MayorMonto(int id)
        {
            var patrocinadorM = await context.Heroes
                .Join(
                context.Patrocinadors,
                x => x.HeroeId,
                p => p.HeroeId,
                (x,p) => new {x,p}
                )
                .Join(
                context.Personas,
                y => y.x.IdPersona,
                ps => ps.Id, (y,ps) => new {y,ps}
                ).Where(k => k.y.x.HeroeId.Equals(id))
                .GroupBy(z => new { z.y.x.HeroeId, z.y.p.PId, z.y.p.Monto, z.y.p.OrigenDinero})
                .Select(rs => new {
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
