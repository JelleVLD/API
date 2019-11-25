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
    public class PollGebruikerController : ControllerBase
    {
        private readonly APIContext _context;

        public PollGebruikerController(APIContext context)
        {
            _context = context;
        }

        // haalt alle pollGebruikers op 
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PollGebruiker>>> GetPollGebruikers()
        {
            return await _context.PollGebruikers.Include(g => g.gebruiker).Include(p => p.poll).ToListAsync();
        }

        //haalt de pollgebruiker met gebruiker en poll op met de meegegeven id
        [Authorize]
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

        // past de pollgebruiker aan met het meegegeven id
        [Authorize]
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

        //voegt een nieuwe pollgebruiker toe
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<PollGebruiker>> PostPollGebruiker(PollGebruiker pollGebruiker)
        {
            _context.PollGebruikers.Add(pollGebruiker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPollGebruiker", new { id = pollGebruiker.polGebruikerID }, pollGebruiker);
        }

        // delete de pollgebruiker van het meegegeven pollid
        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<PollGebruiker>> DeletePollGebruiker(long id)
        {
            Console.WriteLine(id);
            var pollGebruikers = _context.PollGebruikers.Where(p => p.pollID == id).ToList();

            foreach (var pollGebruiker in pollGebruikers)
            {
                _context.PollGebruikers.Remove(pollGebruiker);
                 _context.SaveChanges();
            }


            return pollGebruikers;
        }
        //controleert of de meegegeven vriendid bestaat in de database
        private bool PollGebruikerExists(long id)
        {
            return _context.PollGebruikers.Any(e => e.polGebruikerID == id);
        }
    }
}
