using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheGuardians.DBContext;
using TheGuardians.Models;

namespace TheGuardians.Controllers
{
    [ApiController]
    [Route("api/villanos")]
    public class VillanosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public VillanosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("Nombre/{nombre}")]
        public async Task<ActionResult<Villano>> ObtenerVillanoNombre([FromRoute] string nombre)
        {
            var villano = await context.Villanos.Include(x => x.Persona)
                .FirstOrDefaultAsync(y => y.Persona.Nombre.Contains(nombre));
                

            if (villano == null)
            {
                return NotFound();
            }

            return villano;
        }

        [HttpGet]
        [Route("Origen/{origen}")]
        public async Task<ActionResult<Villano>> ObtenerVillanoOrigen([FromRoute] string origen)
        {
            var villano = await context.Villanos.Include(x => x.Persona)
                .FirstOrDefaultAsync(y => y.Origen.Contains(origen));


            if (villano == null)
            {
                return NotFound();
            }

            return villano;
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
