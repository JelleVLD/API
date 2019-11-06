using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Gebruiker
    {
        [Key]
        public long gebruikerID { get; set; }
        public string email { get; set; }
        public string wachtwoord { get; set; }
        public string gebruikersnaam { get; set; }
        [NotMapped]
        public string Token { get; set; }
    }
}
