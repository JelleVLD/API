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
    public class PollGebruikerController : ControllerBase
    {
        private readonly APIContext _context;

        public PollGebruikerController(APIContext context)
        {
            _context = context;
        }

        // GET: api/PollGebruiker
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PollGebruiker>>> GetPollGebruikers()
        {
            return await _context.PollGebruikers.Include(g => g.gebruiker).Include(p => p.poll).ToListAsync();
        }

        // GET: api/PollGebruiker/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<PollGebruiker>>> GetPollGebruiker(long id)
        {
            var pollGebruikers = await _context.PollGebruikers.Include(g => g.gebruiker).Include(p => p.poll).Where(i => i.gebruikerID == id).ToListAsync();

            if (pollGebruikers == null)
            {
                return NotFound();
            }

            return pollGebruikers;
        }

        // PUT: api/PollGebruiker/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPollGebruiker(long id, PollGebruiker pollGebruiker)
        {
            if (id != pollGebruiker.polGebruikerID)
            {
                return BadRequest();
            }

            _context.Entry(pollGebruiker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PollGebruikerExists(id))
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

        // POST: api/PollGebruiker
        [HttpPost]
        public async Task<ActionResult<PollGebruiker>> PostPollGebruiker(PollGebruiker pollGebruiker)
        {
            _context.PollGebruikers.Add(pollGebruiker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPollGebruiker", new { id = pollGebruiker.polGebruikerID }, pollGebruiker);
        }

        // DELETE: api/PollGebruiker/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PollGebruiker>> DeletePollGebruiker(long id)
        {
            var pollGebruiker = await _context.PollGebruikers.FindAsync(id);
            if (pollGebruiker == null)
            {
                return NotFound();
            }

            _context.PollGebruikers.Remove(pollGebruiker);
            await _context.SaveChangesAsync();

            return pollGebruiker;
        }

        private bool PollGebruikerExists(long id)
        {
            return _context.PollGebruikers.Any(e => e.polGebruikerID == id);
        }
    }
}
