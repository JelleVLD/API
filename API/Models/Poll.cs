using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Poll
    {
        [Key]
        public long pollID { get; set; }
        public string naam { get; set; }
        public ICollection<Antwoord> antwoorden { get; set; }

    }
}
