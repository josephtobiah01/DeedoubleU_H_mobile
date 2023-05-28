using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrakeWorkwise.Models;
using System.Windows.Input;
using DrakeWorkwise.Interfaces;

namespace DrakeWorkwise.ViewModels
{
    public class CrisisContactViewModel : CallUsViewModelBase
    {
        private IPhoneService _phoneService;
        private readonly IGetDataService _getDataService;

        public CrisisContactViewModel(IPhoneService phoneService, INavigationService nav, IGetDataService getDataService) : base(nav)
        {
            _phoneService = phoneService;
            _getDataService = getDataService;
            Initialize();
        }

        private async void Initialize()
        {
            LoadContactsCommand = new Command<string>((zone) =>
            {
                if (zone.ToString().ToLower() == "au")
                {
                    if (!AuSelected)
                    {
                        NzSelected = false;
                        AuSelected = true;
                        PhoneContactDetails = new List<CrisisContact>(_au);
                    }
                }
                else
                {
                    if (!NzSelected)
                    {
                        NzSelected = true;
                        AuSelected = false;
                        PhoneContactDetails = new List<CrisisContact>(_nz);
                    }
                }
            });

            PhoneCallCommand = new Command<CrisisContact>(async (num) =>
            {
                var res = await ShowConfirmationAsync(num.Title, num.ContactNumber, "Call", "Cancel");
                if (res)
                    _phoneService.PhoneCall(num.ContactNumber);

            });

            //_au = new List<PhoneContactDetails>();
            //_au.Add(new PhoneContactDetails() { Image = "contactus.png", ContactNumber = "13 11 14", Text = "Lifeline" });
            //_au.Add(new PhoneContactDetails() { Image = "contactus.png", ContactNumber = "1300 659 467", Text = "Suicide Prevention Call Back" });
            //_au.Add(new PhoneContactDetails() { Image = "contactus.png", ContactNumber = "1300 22 4636", Text = "Beyond Blue" });
            //_au.Add(new PhoneContactDetails() { Image = "contactus.png", ContactNumber = "0000", Text = "Police and Emergency Services" });

            //_nz = new List<PhoneContactDetails>();
            //_nz.Add(new PhoneContactDetails() { Image = "contactus.png", ContactNumber = "0800 543 354", Text = "Lifeline" });
            //_nz.Add(new PhoneContactDetails() { Image = "contactus.png", ContactNumber = "0508 828 865", Text = "Suicide Crisis Helpline" });
            //_nz.Add(new PhoneContactDetails() { Image = "contactus.png", ContactNumber = "0800 611 116", Text = "Healthline" });
            //_nz.Add(new PhoneContactDetails() { Image = "contactus.png", ContactNumber = "0800 726 666", Text = "Samaritans" });
            //_nz.Add(new PhoneContactDetails() { Image = "contactus.png", ContactNumber = "111", Text = "Police and Emergency Services" });
            //_nz.Add(new PhoneContactDetails() { Image = "contactus.png", ContactNumber = "(09) 5222 999", Text = "Lifeline (within Auckland)" });

            var rawData = await _getDataService.GetRawData();
            _au = rawData.Data.CrisisContact.Where(x => x.Country == "AU" && !x.IsDeleted).ToList();
            _nz = rawData.Data.CrisisContact.Where(x => x.Country == "NZ" && !x.IsDeleted).ToList();

            PhoneContactDetails = new List<CrisisContact>(_au);
            AuSelected = true;
        }

        private List<CrisisContact> _au = null;
        private List<CrisisContact> _nz = null;


        private List<CrisisContact> _phoneContactDetails = null;
        public List<CrisisContact> PhoneContactDetails
        {
            get { return _phoneContactDetails; }
            set { SetPropertyValue(ref _phoneContactDetails, value); }
        }

        private bool _auSelected = false;
        public bool AuSelected
        {
            get { return _auSelected; }
            set { SetPropertyValue(ref _auSelected, value); }
        }

        private bool _nzSelected = false;
        public bool NzSelected
        {
            get { return _nzSelected; }
            set { SetPropertyValue(ref _nzSelected, value); }
        }
        public Command<string> LoadContactsCommand { get; set; }

        public Command<CrisisContact> PhoneCallCommand { get; set; }

    }
}
