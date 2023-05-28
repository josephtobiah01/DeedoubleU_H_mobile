using DrakeWorkwise.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeWorkwise.Models
{
    public class ContactDetails : BindableBase
    {
        private string _name = "";
        public string Name
        {
            get { return _name; }
            set { SetPropertyValue(ref _name, value); }
        }
        private string _email = "";
        public string Email
        {
            get { return _email; }
            set { SetPropertyValue(ref _email, value); }
        }

        private string _contactNumber = "";
        public string ContactNumber
        {
            get { return _contactNumber; }
            set { SetPropertyValue(ref _contactNumber, value); }
        }


        private YouAre _youAre = YouAre.None;
        public YouAre YouAre
        {
            get { return _youAre; }
            set { SetPropertyValue(ref _youAre, value); }
        }


        private string _comments = "";
        public string Comments
        {
            get { return _comments; }
            set { SetPropertyValue(ref _comments, value); }
        }
    }

    public enum YouAre
    {
        None, Employee, Employer, Provider
    }


}
