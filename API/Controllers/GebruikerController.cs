using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
[Route("api/[controller]")]
[ApiController]
    public class GebruikerController: ControllerBase
    {
        private IUserService _gebruikerService;
        private readonly APIContext _context;
        public GebruikerController(IUserService gebruikerService, APIContext context)
        {
            _context = context;
            _gebruikerService = gebruikerService; 
        }
    [HttpPost("authenticate")] 
        public IActionResult Authenticate([FromBody]Gebruiker userParam) 
        { 
            var gebruiker = _gebruikerService.Authenticate(userParam.gebruikersnaam, userParam.wachtwoord);
            if (gebruiker == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            return Ok(gebruiker);
        }
        // GET: api/Gebruiker
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gebruiker>>> GetGebruikers()
        {
            return await _context.Gebruikers.ToListAsync();
        }
        // GET: api/Gebruiker/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gebruiker>> GetGebruiker(long id)
        {
            var gebruiker = await _context.Gebruikers.FindAsync(id);

            if (gebruiker == null)
            {
                return NotFound();
            }

            return gebruiker;
        }
        // PUT: api/Gebruiker/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGebruiker(long id, Gebruiker gebruiker)
        {
            if (id != gebruiker.gebruikerID)
            {
                return BadRequest();
            }

            _context.Entry(gebruiker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GebruikerExists(id))
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
        // POST: api/Gebruiker
        [HttpPost]
        public async Task<ActionResult<Gebruiker>> PostGebruiker(Gebruiker gebruiker)
        {
            _context.Gebruikers.Add(gebruiker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGebruiker", new { id = gebruiker.gebruikerID }, gebruiker);
        }

        // DELETE: api/Antwoord/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Gebruiker>> DeleteGebruiker(long id)
        {
            var gebruiker = await _context.Gebruikers.FindAsync(id);
            if (gebruiker == null)
            {
                return NotFound();
            }

            _context.Gebruikers.Remove(gebruiker);
            await _context.SaveChangesAsync();

            return gebruiker;
        }

        private bool GebruikerExists(long id)
        {
            return _context.Gebruikers.Any(e => e.gebruikerID == id);
        }
    }
}
    

       

