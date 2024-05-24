using infraccionAPI.Data;
using infraccionAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace infraccionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SancionesController : ControllerBase
    {
        private readonly InfraccionVehicularContext _context;

        public SancionesController(InfraccionVehicularContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sancion>>> GetSanciones()
        {
            return await _context.Sanciones.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sancion>> GetSancion(int id)
        {
            var sancion = await _context.Sanciones.FindAsync(id);

            if (sancion == null)
            {
                return NotFound();
            }

            return sancion;
        }

        [HttpPost]
        public async Task<ActionResult<Sancion>> PostSancion(Sancion sancion)
        {
            _context.Sanciones.Add(sancion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSancion", new { id = sancion.SancionID }, sancion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSancion(int id, Sancion sancion)
        {
            if (id != sancion.SancionID)
            {
                return BadRequest();
            }

            _context.Entry(sancion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SancionExists(id))
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
        public async Task<IActionResult> DeleteSancion(int id)
        {
            var sancion = await _context.Sanciones.FindAsync(id);
            if (sancion == null)
            {
                return NotFound();
            }

            _context.Sanciones.Remove(sancion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SancionExists(int id)
        {
            return _context.Sanciones.Any(e => e.SancionID == id);
        }
    }
}
