using DrakeWorkwise.Interfaces;
using DrakeWorkwise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeWorkwise.ViewModels
{
    public class CallUsViewModel : ViewModelBase
    {
        private readonly IPhoneService _phoneService;

        public CallUsViewModel(IPhoneService phoneService, INavigationService nav) : base(nav)
        {
            _phoneService = phoneService;

            PhoneContactDetails = new List<PhoneContactDetails>();
            PhoneContactDetails.Add(new PhoneContactDetails() { ContactNumber = "1300 135 600", Text = "Australia: " });
            PhoneContactDetails.Add(new PhoneContactDetails() { ContactNumber = "0800 452 521", Text = "New Zealand: " });//+61 2 9273 0567
            PhoneContactDetails.Add(new PhoneContactDetails() { ContactNumber = "+61 2 9273 0567", Text = "If you are overseas, please call: " });

            PhoneCallCommand = new Command<PhoneContactDetails>(async (num) =>
            {
                var res = await ShowConfirmationAsync(num.Text, num.ContactNumber, "Call", "Cancel");
                if (res)
                    _phoneService.PhoneCall(num.ContactNumber);
            });
        }


        private List<PhoneContactDetails> _phoneContactDetails = null;
        public List<PhoneContactDetails> PhoneContactDetails
        {
            get { return _phoneContactDetails; }
            set { SetPropertyValue(ref _phoneContactDetails, value); }
        }

        public Command<PhoneContactDetails> PhoneCallCommand { get; set; }
    }
}
