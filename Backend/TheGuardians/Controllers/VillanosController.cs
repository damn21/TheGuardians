using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheGuardians.DBContext;
using TheGuardians.DTOs;
using TheGuardians.Models;

namespace TheGuardians.Controllers
{
    [ApiController]
    [Route("api/villanos")]
    public class VillanosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public VillanosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost("AgregarVillano")]
        public async Task<ActionResult> Post([FromBody] VillanoCreationDTO villanoCreationDTO)
        {
            var villanos = mapper.Map<Villano>(villanoCreationDTO);

            context.Add(villanos);
            await context.SaveChangesAsync();

            var villanoDTO = mapper.Map<VillanoDTO>(villanos);

            return new JsonResult(villanoDTO);

        }

        [HttpGet]
        public async Task<List<VillanoDTO>> GetHeroes()
        {
            var villanos = await context.Villanos.Include(x => x.Persona).ToListAsync();
            return mapper.Map<List<VillanoDTO>>(villanos);
        }

        [HttpGet]
        [Route("Nombre/{nombre}")]
        public async Task<ActionResult<List<VillanoDTO>>> ObtenerVillanoNombre([FromRoute] string nombre)
        {
            var villanos = await context.Villanos.Include(x => x.Persona)
                .Where(y => y.Persona.Nombre.Contains(nombre)).ToListAsync();

            if (villanos == null)
            {
                return NotFound();
            }

            return mapper.Map<List<VillanoDTO>>(villanos);
        }

        [HttpGet]
        [Route("Origen/{origen}")]
        public async Task<ActionResult<List<VillanoDTO>>> ObtenerVillanoOrigen([FromRoute] string origen)
        {
            var villanos = await context.Villanos.Include(x => x.Persona)
                .Where(y => y.Origen.Contains(origen)).ToListAsync();


            if (villanos == null)
            {
                return NotFound();
            }

            return mapper.Map<List<VillanoDTO>>(villanos);
        }

        [HttpGet]
        [Route("Debilidad/{debilidad}")]
        public async Task<ActionResult<Villano>> ObtenerVillanoDebilidad([FromRoute] string debilidad)
        {
            var villano = await context.Villanos.Include(x => x.Persona)
                .FirstOrDefaultAsync(y => y.Persona.Debilidad.Contains(debilidad));


            if (villano == null)
            {
                return NotFound();
            }

            return villano;
        }
    }
}
