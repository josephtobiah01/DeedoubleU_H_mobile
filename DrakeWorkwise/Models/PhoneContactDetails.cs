using DrakeWorkwise.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeWorkwise.Models
{
    public class PhoneContactDetails : LinkBase
    {
        private string _contactNumber = "";
        public string ContactNumber
        {
            get { return _contactNumber; }
            set { SetPropertyValue(ref _contactNumber, value); }
        }
    }
}
