using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class DBInitializer
    {
        public static void Initialize(APIContext context)
        {
            context.Database.EnsureCreated();
            if(context.Gebruikers.Any())
            {
                return;
            }
            else
            {
                context.Gebruikers.AddRange(
                                new Gebruiker {email = "jelle.vld@telenet.be", wachtwoord = "Abc123", gebruikersnaam = "Jelle_VLD" },
                                new Gebruiker { email = "suske@gmail.be", wachtwoord = "Abc123", gebruikersnaam = "Suske" },
                                new Gebruiker { email = "wiske@gmail.be", wachtwoord = "Abc123", gebruikersnaam = "Wiske" },
                                new Gebruiker { email = "gebruiker.gebruiker@gebruiker.be", wachtwoord = "Abc123", gebruikersnaam = "Gebruiker" }
                                ); 
            }
            if (context.Vrienden.Any())
            {
                return;
            }
            else
            {
                context.Vrienden.AddRange(
                                  new Vriend { ZenderID = 1, OntvangerID = 4, bevestigd = true },
                new Vriend { ZenderID = 3, OntvangerID = 1, bevestigd = false },
                new Vriend { ZenderID = 1, OntvangerID = 2, bevestigd = false },
                 new Vriend { ZenderID = 2, OntvangerID = 3, bevestigd = true }
                                );
            }
            if (context.Polls.Any())
            {
                return;
            }
            else
            {
                context.Polls.AddRange(
                                new Poll { naam="Feestje" },
                                new Poll { naam="Vat" }
                                );
            }
            if (context.PollGebruikers.Any())
            {
                return;
            }
            else
            {
                context.PollGebruikers.AddRange(
                                new PollGebruiker { pollID = 1 ,gebruikerID = 2},
                                new PollGebruiker { pollID = 2 ,gebruikerID = 1}
                                );
            }
            if (context.Antwoorden.Any())
            {
                return;
            }
            else
            {
                context.Antwoorden.AddRange(
                                new Antwoord { antwoord = new DateTime(2019,12,13), pollID=1},
                                new Antwoord { antwoord = new DateTime(2020,1,1),pollID=2}
                                );
            }
            if (context.Stemmen.Any())
            {
                return;
            }
            else
            {
                context.Stemmen.AddRange(
                                new Stem { antwoordID =1, gebruikerID=1 },
                                new Stem { antwoordID = 2, gebruikerID= 2}
                                );
            }
            context.SaveChanges();
        }
    }

        }
