using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrakeWorkwise.Interfaces;
namespace DrakeWorkwise.Services
{
    public class PhoneService : IPhoneService
    {
        public PhoneService()
        {

        }

        public void PhoneCall(string number)
        {
            if (PhoneDialer.Default.IsSupported)
                PhoneDialer.Default.Open(number);
        }
    }
}
