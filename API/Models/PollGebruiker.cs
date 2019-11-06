using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class PollGebruiker
    {
        [Key]
        public long polGebruikerID { get; set; }
        public long pollID { get; set; }
        public long gebruikerID{ get; set; }

        public Gebruiker gebruiker { get; set; }
        public Poll poll { get; set; }
    }
}
