using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AntwoordController : ControllerBase
    {
        private readonly APIContext _context;

        public AntwoordController(APIContext context)
        {
            _context = context;
        }

        // GET: api/Antwoord
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Antwoord>>> GetAntwoorden()
        {
            return await _context.Antwoorden.ToListAsync();
        }

        // GET: api/Antwoord/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Antwoord>> GetAntwoord(long id)
        {
            var antwoord = await _context.Antwoorden.FindAsync(id);

            if (antwoord == null)
            {
                return NotFound();
            }

            return antwoord;
        }

        // PUT: api/Antwoord/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAntwoord(long id, Antwoord antwoord)
        {
            if (id != antwoord.antwoordID)
            {
                return BadRequest();
            }

            _context.Entry(antwoord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AntwoordExists(id))
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

        // POST: api/Antwoord
        [HttpPost]
        public async Task<ActionResult<Antwoord>> PostAntwoord(Antwoord antwoord)
        {
            _context.Antwoorden.Add(antwoord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAntwoord", new { id = antwoord.antwoordID }, antwoord);
        }

        // DELETE: api/Antwoord/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Antwoord>> DeleteAntwoord(long id)
        {
            var antwoord = await _context.Antwoorden.FindAsync(id);
            if (antwoord == null)
            {
                return NotFound();
            }

            _context.Antwoorden.Remove(antwoord);
            await _context.SaveChangesAsync();

            return antwoord;
        }

        private bool AntwoordExists(long id)
        {
            return _context.Antwoorden.Any(e => e.antwoordID == id);
        }
    }
}
