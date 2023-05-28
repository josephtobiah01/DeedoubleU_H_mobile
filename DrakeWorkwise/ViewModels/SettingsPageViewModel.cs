using DrakeWorkwise.Interfaces;
using DrakeWorkwise.Models;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DrakeWorkwise.ViewModels
{
    public class SettingsPageViewModel : CallUsViewModelBase
    {
        public SettingsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Initialize();
        }

        private void Initialize()
        {
            SettingsLinks = new List<SettingsLink>()
            {
                new SettingsLink(){ Text = "About Us", Image = "set_about.png", LinkParameter = "WebViewPage|12|About Us" },
                new SettingsLink(){ Text = "Send Feedback", Image = "set_send_feedback.png", LinkParameter = $"ContactUs|{bool.FalseString}" },
                new SettingsLink(){ Text = "Make General Enquiry", Image = "set_make_general_enquiry.png", LinkParameter = $"ContactUs|{bool.TrueString}" },
                new SettingsLink(){ Text = "Terms and Conditions", Image = "set_terms_and_conditions.png", LinkParameter = "WebViewPage|10|Terms and Conditions" },
                new SettingsLink(){ Text = "Privacy Policy", Image = "set_privacy_policy.png", LinkParameter = "WebViewPage|11|Privacy Policy" },
                new SettingsLink(){ Text = "Share App", Image = "set_share_app.png", LinkParameter = "shareapp" },
            };

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
                    else if (link.Contains("ContactUs"))
                    {
                        string[] _data = link.Split('|');
                        Dictionary<string, object> _param = new Dictionary<string, object>();
                        _param.Add("IsContact", Convert.ToBoolean(_data[1]));
                        await NavigationService.NavigateTo(_data[0], _param);
                    }
                    else if (link == "shareapp")
                    {
                        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
                        {
                            await ShareUri("https://play.google.com/store/apps/details?id=com.drakeintl.drakewellbeinghub");
                        }   
                        else if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
                        {
                            await ShareUri("https://apps.apple.com/ph/app/drake-wellbeinghub/id1640651706");
                        }
                            
                    }
                    else
                    {
                        //await NavigateTo($@"{link}");
                        //NavigationService.NavigateTo<>
                        await NavigationService.NavigateTo(link);
                    }


                }
            });
        }

        public async Task ShareUri(string uri)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = uri,
                Title = "Share application link"
            });
        }

        private List<SettingsLink> _settingsLinks = null;
        public List<SettingsLink> SettingsLinks
        {
            get { return _settingsLinks; }
            set
            {
                SetPropertyValue(ref _settingsLinks, value);
            }
        }

        public Command<string> ClickLinkCommand { get; set; }
    }
}
