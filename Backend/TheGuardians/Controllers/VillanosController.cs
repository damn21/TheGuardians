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
    [Produces("application/json")]
    public class VillanosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public VillanosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Agregar villanos
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Retorna lista</response>
        /// <response code="400">Lista vacía</response>
        /// <response code="500">Error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post([FromBody] VillanoCreationDTO villanoCreationDTO)
        {
            var villanos = mapper.Map<Villano>(villanoCreationDTO);

            context.Add(villanos);
            await context.SaveChangesAsync();

            var villanoDTO = mapper.Map<VillanoDTO>(villanos);

            return new JsonResult(villanoDTO);

        }

        /// <summary>
        /// Obtiene lista de todos los villanos registrados
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Retorna lista</response>
        /// <response code="400">Lista vacía</response>
        /// <response code="500">Error</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<List<VillanoDTO>> GetVillanos()
        {
            var villanos = await context.Villanos.Include(x => x.Persona).ToListAsync();
            return mapper.Map<List<VillanoDTO>>(villanos);
        }

        /// <summary>
        /// Obtiener villano por nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        /// <response code="200">Retorna lista</response>
        /// <response code="400">Lista vacía</response>
        /// <response code="500">Error</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{nombre}")]
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

        /// <summary>
        /// Obtiener villano por origen
        /// </summary>
        /// <param name="origen"></param>
        /// <returns></returns>
        /// <response code="200">Retorna lista</response>
        /// <response code="400">Lista vacía</response>
        /// <response code="500">Error</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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


        /// <summary>
        /// Obtiener villano por origen
        /// </summary>
        /// <param name="debilidad"></param>
        /// <returns></returns>
        /// <response code="200">Retorna lista</response>
        /// <response code="400">Lista vacía</response>
        /// <response code="500">Error</response>  
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
