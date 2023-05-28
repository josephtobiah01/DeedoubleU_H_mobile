using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DrakeWorkwise.Models
{
    public class AuthenticationResponse : ApiResponse
    {
        //[JsonProperty("API-Key")]
        public string apiKey { get; set; }

        public override string ToString()
        {
            return "authKey:" + apiKey;
        }
    }

    public class RequestResponse : ApiResponse
    {

        //[JsonProperty("feedback")]
        public bool Feedback { get; set; }
    }

    public class ApiResponse
    {

        //[JsonProperty("code")]
        public int Code { get; set; }

        //[JsonProperty("msg")]
        public string Message { get; set; }
    }


}
