using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
using System.Net;

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
        //stuurt een mail naar het meegegeven mailadres
        [Authorize]
        [HttpPost]
        [Route("stuurmail")]
        public  void SendEmail(Email email)
        {
            MailAddress to = new MailAddress(email.email);
            MailAddress from = new MailAddress("uitnodiging@PollAppster.com");

            MailMessage message = new MailMessage(from, to);
            message.Subject = email.subject;
            message.Body = email.message;
            message.IsBodyHtml = true;

            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("90ff9dd166282d", "da28a388fdd5dd"),
                EnableSsl = true
            };

            try
            {
                client.Send(message);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        //identificeert of er met de meegegeven login een gebruiker bestaat
        [HttpPost("authenticate")] 
        public IActionResult Authenticate([FromBody]Gebruiker userParam) 
        { 
            var gebruiker = _gebruikerService.Authenticate(userParam.gebruikersnaam, userParam.wachtwoord);
            if (gebruiker == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            return Ok(gebruiker);
        }
        // haalt alle gebruikers op
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gebruiker>>> GetGebruikers()
        {
            return await _context.Gebruikers.ToListAsync();
        }
        //haalt de gebruiker op met de meegegeven id
        [Authorize]
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
        // past de gebruiker aan met het meegegeven id
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
        //voegt een nieuwe gebruiker toe
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Gebruiker>> PostGebruiker(Gebruiker gebruiker)
        {
            _context.Gebruikers.Add(gebruiker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGebruiker", new { id = gebruiker.gebruikerID }, gebruiker);
        }

        // delete de gebruiker van het meegegeven id
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
        //controleert of de meegegeven gebruikerid bestaat in de database

        private bool GebruikerExists(long id)
        {
            return _context.Gebruikers.Any(e => e.gebruikerID == id);
        }
    }
}
    

       

