using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class APIContext: DbContext{
        public APIContext(DbContextOptions<APIContext> options) : base(options)
        {
        
        }
    public DbSet<Gebruiker> Gebruikers { get; set; }
    public DbSet<Poll> Polls { get; set; }
    public DbSet<Stem> Stemmen { get; set; }
    public DbSet<Antwoord>Antwoorden { get; set; }
    public DbSet<PollGebruiker> PollGebruikers { get; set; }
        public DbSet<Vriend> Vrienden { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {

            modelBuilder.Entity<Gebruiker>().ToTable("Gebruiker");
            modelBuilder.Entity<Poll>().ToTable("Poll");
            modelBuilder.Entity<Stem>().ToTable("Stem");
            modelBuilder.Entity<Antwoord>().ToTable("Antwoord");
            modelBuilder.Entity<PollGebruiker>().ToTable("PollGebruiker");
            modelBuilder.Entity<Vriend>().ToTable("Vriend");

        }

    }
}
