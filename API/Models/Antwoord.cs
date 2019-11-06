using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Antwoord
    {
        [Key]
        public long antwoordID { get; set; }
        public DateTime antwoord{ get; set; }
        public long pollID { get; set; }

    }
}
