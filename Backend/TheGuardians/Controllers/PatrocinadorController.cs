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

        //[HttpGet]
        //public async Task<ActionResult<List<Patrocinador>>> MayorMonto(int id)
        //{
        //    var heroe = await context.Patrocinadors.
        //}

    }
}
