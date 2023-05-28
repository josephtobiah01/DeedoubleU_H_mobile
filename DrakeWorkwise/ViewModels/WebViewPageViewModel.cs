using DrakeWorkwise.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeWorkwise.ViewModels
{
    public class WebViewPageViewModel : CallUsViewModelBase, IPhoneService
    {
        private readonly IGetDataService _getDataService;
        private readonly IPhoneService _phoneService;

        public WebViewPageViewModel(INavigationService navigationService, IGetDataService getDataService, IPhoneService phoneService) : base(navigationService)
        {
            _getDataService = getDataService;
            _phoneService = phoneService;
            Initialize();
        }

        private async void Initialize()
        {
            _id = "7";
            Title = "EAP Privacy";

            _phoneNumbers = new List<string>();
            _phoneNumbers.Add("1300 135 600");
            _phoneNumbers.Add("0800 452 521");
            _phoneNumbers.Add(" 000");
            _phoneNumbers.Add(" 111");
            _phoneNumbers.Add("1300 363 992");
            _phoneNumbers.Add("13 11 14");
            //_phoneNumbers.Add("1800");
            _phoneNumbers.Add("+61 2 9273 0563");


            await LoadPage();
        }


        public string FormatHTMLAddPhoneLink(string html)
        {
            string ret = html;
            foreach (var number in _phoneNumbers)
            {
                ret = ret.Replace(number, AddPhoneLink(number));
            }
            return ret;
        }

        private string AddPhoneLink(string phoneNumber)
        {
            return $"\t\r\n<a data-rel=\"external\" href=\"tel:{phoneNumber.Replace(" ", "")}\">{phoneNumber}</a>";
        }


        private List<string> _phoneNumbers = new List<string>();

        private string _id = "7";

        private string _imgSource = "";
        public string ImgSource
        {
            get { return _imgSource; }
            set
            {
                SetPropertyValue(ref _imgSource, value);
            }
        }

        private string _html = "";
        public string HTML
        {
            get { return _html; }
            set
            {
                SetPropertyValue(ref _html, value);
            }
        }

        public Command GoBackCommand { get; private set; }

        public override async Task OnNavigatingTo(Dictionary<string, object> parameter)
        {

            if (parameter.TryGetValue("id", out object id))
            {
                _id = id.ToString();
                await LoadPage();
            }

            if (parameter.TryGetValue("title", out object title))
            {
                Title = title.ToString();
            }

            if (parameter.TryGetValue("content", out object content))
            {
                //HTML = DeviceInfo.Current.Platform == DevicePlatform.iOS ? $"<font size=\"+5\">{content.ToString()}</font>" : content.ToString();
                HTML = FormatContent(content.ToString());
            }

            if (parameter.TryGetValue("image", out object image))
            {
                ImgSource = image.ToString();
            }

        }

        private async Task LoadPage()
        {
            var _data = await _getDataService.GetRawData();
            var _page = _data.Data.Pages.FirstOrDefault(x => x.Id.ToString() == _id);
            //HTML = DeviceInfo.Current.Platform == DevicePlatform.iOS ? $"<font size=\"+5\">{_page.Content}</font>" : _page.Content; 
            HTML = FormatContent(_page.Content);
            ImgSource = _page.Image;
        }

        private string FormatContent(string html)
        {
            string ret = "";
            ret = $"<meta name=\"format-detection\" content=\"telephone-yes\"><p style=\"font-family:verdana\">{html}</p></meta>";//<body style=\"{(DeviceInfo.Current.Platform == DevicePlatform.iOS ? "margin:60px;" : "margin:30px;")}\">
            ret = FormatHTMLAddPhoneLink(ret);
            //<p style="font-family:verdana">
            ret = DeviceInfo.Current.Platform == DevicePlatform.iOS && DeviceInfo.Current.Idiom == DeviceIdiom.Phone ? $"<font size=\"+5\">{ret}</font>" : ret;
            return ret;
        }

        public async void PhoneCall(string number)
        {
            var res = await ShowConfirmationAsync(null, number, "Call", "Cancel");
            if (res)
                _phoneService.PhoneCall(number);
            //_phoneService.PhoneCall(number);
        }
    }
}
