using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Vriend
    {
        [Key]
        public long vriendenID { get; set; }
        public long? ZenderID { get; set; }
        public long? OntvangerID { get; set; }
        public bool bevestigd { get; set; }
        public Gebruiker Zender { get; set; }
        public Gebruiker Ontvanger { get; set; }
    }
}
