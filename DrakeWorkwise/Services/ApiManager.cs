using DrakeWorkwise.Interfaces;
using DrakeWorkwise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Http;
using System.Net;
using Newtonsoft.Json;

namespace DrakeWorkwise.Services
{
    public class ApiManager : IApiManager
    {
        private string baseUrl = "https://drakewellbeinghub.com.au/";

        HttpClient _client = null;
        private readonly HttpClientHandler _clientHandler;
        private readonly CookieContainer _cookieContainer;
        public ApiManager()
        {
            _cookieContainer = new CookieContainer();
            _clientHandler = new HttpClientHandler() { CookieContainer = _cookieContainer };
            _client = new HttpClient(_clientHandler);
            _client.BaseAddress = new Uri(baseUrl);
        }

        public async Task<IList<AuthenticationResponse>> GetAuthKey(Authentication authentication)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add(nameof(authentication.device_id), authentication.device_id);
            parameters.Add(nameof(authentication.device_type), authentication.device_type);
            parameters.Add(nameof(authentication.device_token), authentication.device_token);
            return null;
        }

        public async Task<T> PostForResponseAsync<T>(string uriRequest, Dictionary<string, string> parameters, CancellationToken cancellationToken = default)
        {
            T response = default(T);

            NetworkAccess accessType = Connectivity.Current.NetworkAccess;

            if (accessType == NetworkAccess.Internet)
            {
                var content = new FormUrlEncodedContent(parameters);
                HttpResponseMessage httpResponse = await _client.PostAsync(uriRequest, content, cancellationToken);


                if (httpResponse.IsSuccessStatusCode)
                {
                    var res = await httpResponse.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<T>(res);
                }
            }
            else
            {
                throw new NoInternetException();
            }




            return response;
        }
    }

    public static class ApiHelper
    {
        public static string QueryBuilder(string apiCommand, Dictionary<string, string> parameters)
        {
            string ret = $"{apiCommand}?";
            foreach (var param in parameters)
            {
                ret += $"{param.Key}={new UriBuilder(param.Value).ToString()}&";
            }



            return ret;
        }
    }
}
