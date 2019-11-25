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
    public class VriendenController : ControllerBase
    {
        private readonly APIContext _context;

        public VriendenController(APIContext context)
        {
            _context = context;
        }

        // haalt alle vrienden op 
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vriend>>> GetVrienden()
        {
            return await _context.Vrienden.ToListAsync();
        }
        //haalt de vriend op met de meegegeven id
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Vriend>>> GetVriend(long id)
        {
            var vriend = await _context.Vrienden.Where(z => z.ZenderID == id ||  z.OntvangerID == id).Include(z => z.Zender).Include(o=>o.Ontvanger).ToListAsync();

            if (vriend == null)
            {
                return NotFound();
            }

            return vriend;
        }
        // past de vriend aan met het meegegeven id
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVriend(long id, Vriend vriend)
        {
            if (id != vriend.vriendenID)
            {
                return BadRequest();
            }

            _context.Entry(vriend).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VriendExists(id))
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

        //voegt een nieuwe vriend toe
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Vriend>> PostVriend(Vriend vriend)
        {
            _context.Vrienden.Add(vriend);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVriend", new { id = vriend.vriendenID }, vriend);
        }

        // delete de vriend van het meegegeven id
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vriend>> DeleteVriend(long id)
        {
            var vriend = await _context.Vrienden.FindAsync(id);
            if (vriend == null)
            {
                return NotFound();
            }

            _context.Vrienden.Remove(vriend);
            await _context.SaveChangesAsync();

            return vriend;
        }
        //controleert of de meegegeven vriendid bestaat in de database
        private bool VriendExists(long id)
        {
            return _context.Vrienden.Any(e => e.vriendenID == id);
        }
    }
}
