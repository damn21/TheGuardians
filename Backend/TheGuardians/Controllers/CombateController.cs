using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using TheGuardians.DBContext;
using TheGuardians.DTOs;
using TheGuardians.Models;

namespace TheGuardians.Controllers
{
    [ApiController]
    [Route("api/combate")]
    public class CombateController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CombateController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Combate>>> GetAll()
        {
            return await context.Combates.Include(x => x.Heroe)
                .Include(y => y.Villano).ToListAsync();
        }

        [HttpGet]
        [Route("heroes/victorias")]
        public async Task<ActionResult<List<Combate>>> Mayor()
        {

             var heroesM = await context.Combates.Where(z => z.Resultado.Contains("Ganador"))
                .GroupBy(x => x.HeroeId)
                        .Select(group => new {
                            Heroe = group.Key,
                            Victorias = group.Count()
                        })
                        
                        .OrderByDescending(y => y.Victorias).Take(3).ToListAsync();

            return Ok(heroesM);

        }

        [HttpGet]
        [Route("villano/derrota")]
        public async Task<ActionResult<List<Combate>>> MayorV()
        {

            var villano = await context.Combates
               .Join(context.Heroes, 
               c => c.HeroeId,
               h => h.HeroeId, (c, h) => new { c, h }
               )
               .Join(context.Personas,
               p => p.h.IdPersona,
               p1 => p1.Id, (p,p1) => new {p,p1}
               )
               .Where(z => z.p.c.Resultado.Contains("Ganador"))
               .Where(p1 => p1.p1.Edad<21)
               .GroupBy(x => new { x.p.c.VillanoId, x.p.c.HeroeId, x.p1.Apodo, x.p1.Edad })
                       .Select(y => new {
                           y.Key.VillanoId,
                           y.Key.HeroeId,
                           Edad_Heroe = y.Key.Edad,
                           Nombre_Heroe = y.Key.Apodo,
                           Count = y.Count()
                       })

                       .OrderByDescending(y => y.Count).Take(1).ToListAsync();

            return Ok(villano);

        }

        [HttpGet]
        [Route("heroes/peleas")]
        public async Task<ActionResult<List<Combate>>> Peleas()
        {

            var heroesM = await context.Combates.GroupBy(x => new { x.HeroeId, x.VillanoId })
                       .Select(y => new {
                           y.Key.HeroeId,
                           y.Key.VillanoId,
                           Count = y.Count()
                       })
                       .OrderByDescending(y => y.Count).Take(1).ToListAsync();

            return Ok(heroesM);

        }

    }
}
