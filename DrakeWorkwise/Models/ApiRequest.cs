using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeWorkwise.Models
{
    public class Authentication
    {
        public string device_id { get; set; }
        public string device_type { get; set; }
        public string device_token { get; set; }
    }

    public class Authenticated
    {
        public string api_key { get; set; }
    }

    public class GetData : Authenticated
    {
        public string timestamp { get; set; }

    }

    public class Appointment
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Employer { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Message { get; set; }
        public string PreferredDays { get; set; }
        public string PreferredTime { get; set; }
        public string RequestType { get; set; }
    }

    public class ContactUs
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public string Comments { get; set; }
    }
}
