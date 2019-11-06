using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Stem
    {
        [Key]
        public long stemID { get; set; }
        public long antwoordID { get; set; }
        public long gebruikerID { get; set; }
    }
}
