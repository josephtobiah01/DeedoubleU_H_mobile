using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrakeWorkwise.Models;
using DrakeWorkwise.Interfaces;

namespace DrakeWorkwise.ViewModels
{
    public class HomePageViewModel : CallUsViewModelBase
    {
        private readonly IApiManager _apiManager;
        private readonly IGetDataService _getDataService;

        public HomePageViewModel(INavigationService navigationService, IPhoneService phoneService, IApiManager apiManager, IGetDataService getDataService) : base(navigationService)
        {

            _apiManager = apiManager;
            _getDataService = getDataService;


            Initialize();
        }

        async void Initialize()
        {
            await _apiManager.GetAuthKey(new Authentication()
            {
                device_id = "12",
                device_type = "2",
                device_token = "erwe"
            });

            //HomeLinks = new List<HomeLink>();
            //HomeLinks.Add(new HomeLink() { Text = "What is EAP?", Image = "eap.png", LinkParameter = "WebViewPage|1|What is EAP?" });
            //HomeLinks.Add(new HomeLink() { Text = "Appointments", Image = "appointments.png", LinkParameter = "AppointmentPage" });
            //HomeLinks.Add(new HomeLink() { Text = "Contact Us", Image = "contactus.png", LinkParameter = "ContactUs" });
            //HomeLinks.Add(new HomeLink() { Text = "Crisis Contact", Image = "crisis.png", LinkParameter = "CrisisContactPage" });
            //HomeLinks.Add(new HomeLink() { Text = "Mindfulness", Image = "mindfulness.png", LinkParameter = "WebViewPage|8|Mindfulness" });
            //HomeLinks.Add(new HomeLink() { Text = "Resources", Image = "resources.png", LinkParameter = "ResourcesPage" });
            //HomeLinks.Add(new HomeLink() { Text = "Login", Image = "login.png", LinkParameter = "Link|Login" });
            //HomeLinks.Add(new HomeLink() { Text = "Sign Up", Image = "sign_up.png", LinkParameter = "Link|SignUp" });

            WhatIsEAP = new HomeLink() { Text = "What is EAP?", Image = "eap.png", LinkParameter = "WebViewPage|1|What is EAP?" };
            Appointments = new HomeLink() { Text = "Appointments", Image = "appointments.png", LinkParameter = "AppointmentPage" };
            ContactUs = new HomeLink() { Text = "Contact Us", Image = "contactus.png", LinkParameter = "ContactUs" };
            CrisisContact = new HomeLink() { Text = "Crisis Contact", Image = "crisis.png", LinkParameter = "CrisisContactPage" };
            Mindfullness = new HomeLink() { Text = "Mindfulness", Image = "mindfulness.png", LinkParameter = "WebViewPage|8|Mindfulness" };
            Resources = new HomeLink() { Text = "Resources", Image = "resources.png", LinkParameter = "ResourcesPage" }; //  WellbeingHubPage
            Login = new HomeLink() { Text = "Login", Image = "login.png", LinkParameter = "Link|Login" };
            SignUp = new HomeLink() { Text = "Sign Up", Image = "sign_up.png", LinkParameter = "Link|SignUp" };


            ClickLinkCommand = new Command<string>(async (link) =>
            {
                if (!string.IsNullOrEmpty(link))
                {
                    if (link.Contains("WebViewPage"))
                    {
                        string[] _data = link.Split('|');
                        Dictionary<string, object> _param = new Dictionary<string, object>();
                        _param.Add("id", _data[1]);
                        _param.Add("title", _data[2]);
                        await NavigationService.NavigateTo(_data[0], _param);
                    }
                    else if (link.Equals("AppointmentPage"))
                    {
                        MessagingCenter.Send(this, "appointments");
                    }
                    else if (link.Equals("WellbeingHubPage"))
                    {
                        Uri uri = new Uri("https://drakewellbeinghub.com.au/portal");
                        await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
                    }
                    else if (link.Substring(0, 4) == "Link")
                    {
                        string[] _data = link.Split('|');
                        await ShowAlertAsync("Notice", $"{_data[1]} not yet supported");
                    }
                    else
                    {
                        //await NavigateTo($@"{link}");
                        //NavigationService.NavigateTo<>
                        await NavigationService.NavigateTo(link);
                    }

         
                }
            });

            var data = await _getDataService.GetRawData();
        }

        public override void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Title = DecodeParameter<string>("testing", query);
            base.ApplyQueryAttributes(query);
        }

        //private List<HomeLink> _homeLinks = null;
        

        //public List<HomeLink> HomeLinks
        //{
        //    get { return _homeLinks; }
        //    set { SetPropertyValue(ref _homeLinks, value); }
        //}


        private HomeLink _whatIsEAP = null;
        public HomeLink WhatIsEAP
        {
            get { return _whatIsEAP; }
            set { SetPropertyValue(ref _whatIsEAP, value); }
        }

        private HomeLink _appointments = null;
        public HomeLink Appointments
        {
            get { return _appointments; }
            set { SetPropertyValue(ref _appointments, value); }
        }

        private HomeLink _contactUs = null;
        public HomeLink ContactUs
        {
            get { return _contactUs; }
            set { SetPropertyValue(ref _contactUs, value); }
        }

        private HomeLink _crisisContact = null;
        public HomeLink CrisisContact
        {
            get { return _crisisContact; }
            set { SetPropertyValue(ref _crisisContact, value); }
        }

        private HomeLink _mindfullness = null;
        public HomeLink Mindfullness
        {
            get { return _mindfullness; }
            set { SetPropertyValue(ref _mindfullness, value); }
        }

        private HomeLink _resources = null;
        public HomeLink Resources
        {
            get { return _resources; }
            set { SetPropertyValue(ref _resources, value); }
        }

        private HomeLink _login = null;
        public HomeLink Login
        {
            get { return _login; }
            set { SetPropertyValue(ref _login, value); }
        }

        private HomeLink _signup = null;
        public HomeLink SignUp
        {
            get { return _signup; }
            set { SetPropertyValue(ref _signup, value); }
        }

        public Command<string> ClickLinkCommand { get; set; }
    }
}
