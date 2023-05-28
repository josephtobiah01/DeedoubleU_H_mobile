using DrakeWorkwise.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DrakeWorkwise.ViewModels
{
    public class WellbeingHubPageViewModel : CallUsViewModelBase
    {
        public WellbeingHubPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            if (string.IsNullOrEmpty(URL))
                URL = "https://drakewellbeinghub.com.au/portal";

            Title = null;
        }

        private static string _url = "";
        public string URL
        {
            get { return _url; }
            set
            {
                SetPropertyValue(ref _url, value);
            }
        }

        private static CookieContainer _cookie = null;
        public CookieContainer Cookie
        {
            get { return _cookie; }
            set
            {
                SetPropertyValue(ref _cookie, value);
            }
        }
    }
}
