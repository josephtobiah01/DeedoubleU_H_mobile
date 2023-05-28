using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeWorkwise.Models
{
    public class CountriesCities
    {
        [JsonProperty("cs")]
        public List<CountryDetails> CountryDetails { get; set; } = new List<CountryDetails>();
    }


    public class CountryDetails 
    {
        [JsonProperty("cy")]
        public string country { get; set; }

        [JsonProperty("ct")]
        public string[] cities { get; set; }
    }
}
