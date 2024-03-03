using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryContinentLibrary
{
    public class BigContinent 
    {
        public int id { get; set; }
        public string? NameContinent { get; set; }  
        public virtual ICollection<Country>? Countries { get; set; }
    }
}
