using DrakeWorkwise.Interfaces;
using DrakeWorkwise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;

namespace DrakeWorkwise.Services
{
    public class GetDataService : IGetDataService
    {
        ~GetDataService()
        {
                
        }

        private GetDataRaw _raw = null;
        private CountriesCities _countriesCities = null;
        private List<string> _countries = null;

        public async Task<GetDataRaw> GetRawData()
        {
            if (_raw == null)
            {
                string data = await GetResource("get_data.json");
                _raw = JsonConvert.DeserializeObject<GetDataRaw>(data);
            }
            return await Task.FromResult<GetDataRaw>(_raw);
            
        }

        private async Task<string> GetResource(string path)
        {
            try
            {
                //var assembly = typeof(App).GetTypeInfo().Assembly;
                //var assemblyName = assembly.GetName().Name;

                //var stream = assembly.GetManifestResourceStream($"{assemblyName}.{path}");

                using var stream = await FileSystem.OpenAppPackageFileAsync(path);
                using var reader = new StreamReader(stream);

                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public async Task<CountriesCities> GetCountriesAndCities()
        {
            if (_countriesCities == null)
            {
                string data = await GetResource("countires_cities.json");
                _countriesCities = JsonConvert.DeserializeObject<CountriesCities>(data);
            }
            return await Task.FromResult(_countriesCities);

        }

        public async Task<List<string>> GetCountries()
        {
            if (_countries == null)
            {
                string data = await GetResource("countries.json");
                _countries = JsonConvert.DeserializeObject<List<string>>(data);
            }
            return await Task.FromResult(_countries);
        }

        public async Task<List<string>> GetCities(string country)
        {
            await GetCountriesAndCities();
            List<string> cities = null;
            cities = _countriesCities.CountryDetails.FirstOrDefault(x => x.country.ToLower() == country.ToLower())?.cities?.ToList();
            return cities;
        }
    }
}
