using infraccionAPI.Data;
using infraccionAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace infraccionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfraccionesController : ControllerBase
    {
        private readonly InfraccionVehicularContext _context;

        public InfraccionesController(InfraccionVehicularContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Infraccion>>> GetInfracciones()
        {
            return await _context.Infracciones
                .Include(i => i.Vehiculo)
                .Include(i => i.Conductor)
                .Include(i => i.Sancion)
                .Include(i => i.AgenteTransito)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Infraccion>> GetInfraccion(int id)
        {
            var infraccion = await _context.Infracciones
                .Include(i => i.Vehiculo)
                .Include(i => i.Conductor)
                .Include(i => i.Sancion)
                .Include(i => i.AgenteTransito)
                .FirstOrDefaultAsync(i => i.InfraccionID == id);

            if (infraccion == null)
            {
                return NotFound();
            }

            return infraccion;
        }

        [HttpPost]
        public async Task<ActionResult<Infraccion>> PostInfraccion(Infraccion infraccion)
        {
            _context.Infracciones.Add(infraccion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInfraccion", new { id = infraccion.InfraccionID }, infraccion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInfraccion(int id, Infraccion infraccion)
        {
            if (id != infraccion.InfraccionID)
            {
                return BadRequest();
            }

            _context.Entry(infraccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InfraccionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInfraccion(int id)
        {
            var infraccion = await _context.Infracciones.FindAsync(id);
            if (infraccion == null)
            {
                return NotFound();
            }

            _context.Infracciones.Remove(infraccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InfraccionExists(int id)
        {
            return _context.Infracciones.Any(e => e.InfraccionID == id);
        }
    }
}
