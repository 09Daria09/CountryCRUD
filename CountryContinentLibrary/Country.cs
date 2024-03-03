using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryContinentLibrary
{
    public class Country 
    {
        public int Id { get; set; }
        public string? NameCountry { get; set; }
        public string? Capital { get; set; } 
        public long? Population { get; set; }
        public double? Area { get; set; }
        public virtual BigContinent? BigContinents { get; set; } 
    }
}
