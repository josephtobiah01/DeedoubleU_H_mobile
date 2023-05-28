using DrakeWorkwise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeWorkwise.Interfaces
{
    public interface IGetDataService
    {
        Task<GetDataRaw> GetRawData();
        Task<CountriesCities> GetCountriesAndCities();
        Task<List<string>> GetCountries();

        Task<List<string>> GetCities(string country);
    }
}
