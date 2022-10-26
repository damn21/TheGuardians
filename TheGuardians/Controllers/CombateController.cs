using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpPost]
        public async Task<ActionResult> Post(CombateCreationDTO combateCreationDTO)
        {
            var combate = mapper.Map<Combate>(combateCreationDTO);
            context.Add(combate);
            await context.SaveChangesAsync();
            return Ok();

        }

        [HttpGet]
        public async Task<ActionResult<List<Combate>>> GetAll()
        {
            return await context.Combates.Include(x => x.Heroe)
                .Include(y => y.Villano).ToListAsync();
        }
    }
}
