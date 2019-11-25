using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using Microsoft.AspNetCore.Authorization;

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

        // haalt alle antwoorden op 
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Antwoord>>> GetAntwoorden()
        {
            return await _context.Antwoorden.ToListAsync();
        }

        //haalt de stem op met de meegegeven id
        [Authorize]
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

        // past het antwoord aan met het meegegeven id
        [Authorize]
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

        //voegt een nieuw antwoord toe
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Antwoord>> PostAntwoord(Antwoord antwoord)
        {
            _context.Antwoorden.Add(antwoord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAntwoord", new { id = antwoord.antwoordID }, antwoord);
        }

        // delete het antwoord van het meegegeven id
        [Authorize]
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
        //controleert of de meegegeven antwoordid bestaat in de database

        private bool AntwoordExists(long id)
        {
            return _context.Antwoorden.Any(e => e.antwoordID == id);
        }
    }
}
