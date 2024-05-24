using infraccionAPI.Data;
using infraccionAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace infraccionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentesTransitoController : ControllerBase
    {
        private readonly InfraccionVehicularContext _context;

        public AgentesTransitoController(InfraccionVehicularContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgenteTransito>>> GetAgentesTransito()
        {
            return await _context.AgentesTransito.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AgenteTransito>> GetAgenteTransito(int id)
        {
            var agente = await _context.AgentesTransito.FindAsync(id);

            if (agente == null)
            {
                return NotFound();
            }

            return agente;
        }

        [HttpPost]
        public async Task<ActionResult<AgenteTransito>> PostAgenteTransito(AgenteTransito agente)
        {
            _context.AgentesTransito.Add(agente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAgenteTransito", new { id = agente.AgenteID }, agente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgenteTransito(int id, AgenteTransito agente)
        {
            if (id != agente.AgenteID)
            {
                return BadRequest();
            }

            _context.Entry(agente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgenteTransitoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgenteTransito(int id)
        {
            var agente = await _context.AgentesTransito.FindAsync(id);
            if (agente == null)
            {
                return NotFound();
            }

            _context.AgentesTransito.Remove(agente);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool AgenteTransitoExists(int id)
        {
            return _context.AgentesTransito.Any(e => e.AgenteID == id);
        }
    }
}