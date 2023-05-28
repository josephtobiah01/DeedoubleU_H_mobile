using DrakeWorkwise.Interfaces;
using DrakeWorkwise.Models;
using DrakeWorkwise.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeWorkwise.ViewModels
{
    public class AppointmentViewModel : CallUsViewModelBase
    {
        private readonly IGetDataService _getDataService;
        private readonly IWorkwiseService _workwiseService;
        private readonly IPhoneService _phoneService;

        public AppointmentViewModel(INavigationService nav, IGetDataService getDataService, IWorkwiseService workwiseService, IPhoneService phoneService) : base(nav)
        {
            _getDataService = getDataService;
            _workwiseService = workwiseService;
            _phoneService = phoneService;
            Initialize();
        }

        ~AppointmentViewModel()
        {
            MessagingCenter.Unsubscribe<MessageEditorViewModel>(this, "appointment");
        }

        private void Initialize()
        {
            SubmitCommand = new Command(ExecuteSubmit);
            CheckCommand = new Command<string>(ExecuteCheckCommand);
            SearchCommand = new Command<string>(ExecuteSearchCommand);
            SetMessageCommand = new Command(async () =>
            {
                Message.ClearError();
                Dictionary<string, object> _param = new Dictionary<string, object>();
                _param.Add("from", "appointment");
                _param.Add("message", Message.Value);
                await NavigationService.NavigateTo(nameof(Views.MessageEditorPage), _param);
            });

            CallCommand = new Command<string>(async (num) =>
            {
                var det = num.Split('|');
                if (det.Length > 2)
                {
                    var res = await ShowActionSheetAsync("Call", "Cancel", null, det[0], det[2]);
                    if (!string.IsNullOrEmpty(res) && res != "Cancel")
                        _phoneService.PhoneCall(res == det[0] ? det[1] : det[3]);
                }
                else
                {
                    var res = await ShowConfirmationAsync(det[0], det[1], "Call", "Cancel");
                    if (res)
                        _phoneService.PhoneCall(det[1]);
                }
            });


            MessagingCenter.Subscribe<MessageEditorViewModel>(this, "appointment", (vm) =>
            {
                Message.Value = vm.Value;
                Message.Validate();
            });

            _getDataService.GetCountries().ContinueWith((x) =>
            {
                _countries = x.Result;
            });

            _countryCallBack = new Action<string>((country) =>
            {
                City.Value = country == Country.Value ? City.Value : "";
                Country.Value = country;
                _getDataService.GetCities(Country.Value).ContinueWith((x) =>
                {
                    _cities = x.Result;
                });
            });

            _cityCallBack = new Action<string>((city) =>
            {
                City.Value = city;
            });

            Name = new ValidatableObject<string>() { Title = "Name", Placeholder = "Name" };
            Name.Rules.Add(new ValidNameValidationRule() { ValidationMessage = "Invalid name." });
            Name.Rules.Add(new TextLimitValidationRule() { MaxChar = 200 });
            Name.Rules.Add(new EmptyOrNullValidationRule() { ValidationMessage = "Name must not be empty." });

            Email = new ValidatableObject<string>() { Title = "Email", Placeholder = "email@mail.com", KeyBoard = Keyboard.Email };
            Email.Rules.Add(new EmailValidationRule() { ValidationMessage = "Invalid email." });
            Email.Rules.Add(new EmptyOrNullValidationRule() { ValidationMessage = "Email must not be empty." });

            ContactNumber = new ValidatableObject<string>() { Title = "Contact Number", Placeholder = "Contact Number", KeyBoard = Keyboard.Telephone };
            //ContactNumber.Rules.Add(new PhoneNumberValidation() { ValidationMessage = "Invalid contact number." });
            ContactNumber.Rules.Add(new EmptyOrNullValidationRule() { ValidationMessage = "Contact Number must not be empty" });

            Employer = new ValidatableObject<string>() { Title = "Employer", Placeholder = "Employer" };
            Employer.Rules.Add(new ValidNameValidationRule() { ValidationMessage = "Invalid employer." });
            Employer.Rules.Add(new TextLimitValidationRule() { MaxChar = 200 });
            Employer.Rules.Add(new EmptyOrNullValidationRule() { ValidationMessage = "Employer must not be empty." });

            //Location = new ValidatableObject<string>() { Title = "Location", Placeholder = "Location" };
            //Location.Rules.Add(new ValidNameValidationRule() { ValidationMessage = "Invalid location." });
            //Location.Rules.Add(new TextLimitValidationRule() { MaxChar = 200 });
            //Location.Rules.Add(new EmptyOrNullValidationRule() { ValidationMessage = "Location must not be empty." });
            Country = new ValidatableObject<string>() { Title = "Country", Placeholder = "Country" };
            Country.Rules.Add(new EmptyOrNullValidationRule() { ValidationMessage = "Country must not be empty." });

            City = new ValidatableObject<string>() { Title = "City", Placeholder = "City" };
            City.Rules.Add(new EmptyOrNullValidationRule() { ValidationMessage = "City must not be empty." });

            Message = new ValidatableObject<string>() { Placeholder = "Message" };
            //Message.Rules.Add(new AlphanumericValidation() { ValidationMessage = "Invalid message." });
            Message.Rules.Add(new TextLimitValidationRule() { MaxChar = 500 });
            Message.Rules.Add(new EmptyOrNullValidationRule() { ValidationMessage = "Message must not be empty." });
        }



        private string _content = "";

        public string Content
        {
            get { return _content; }
            set
            {
                SetPropertyValue(ref _content, value);
            }
        }

        private ValidatableObject<string> _name = null;
        public ValidatableObject<string> Name
        {
            get { return _name; }
            set { SetPropertyValue(ref _name, value); }
        }

        private ValidatableObject<string> _email = null;
        public ValidatableObject<string> Email
        {
            get { return _email; }
            set { SetPropertyValue(ref _email, value); }
        }

        private ValidatableObject<string> _contactNumber = null;
        public ValidatableObject<string> ContactNumber
        {
            get { return _contactNumber; }
            set { SetPropertyValue(ref _contactNumber, value); }
        }

        private ValidatableObject<string> _employer = null;
        public ValidatableObject<string> Employer
        {
            get { return _employer; }
            set { SetPropertyValue(ref _employer, value); }
        }

        //private ValidatableObject<string> _location = null;
        //public ValidatableObject<string> Location
        //{
        //    get { return _location; }
        //    set { SetPropertyValue(ref _location, value); }
        //}

        List<string> _countries = null;
        List<string> _cities = null;

        private ValidatableObject<string> _country = null;
        public ValidatableObject<string> Country
        {
            get { return _country; }
            set { SetPropertyValue(ref _country, value); }
        }

        private ValidatableObject<string> _city = null;
        public ValidatableObject<string> City
        {
            get { return _city; }
            set { SetPropertyValue(ref _city, value); }
        }

        //private string _country = "";
        //public string Country
        //{
        //    get { return _country; }
        //    set
        //    {
        //        SetPropertyValue(ref _country, value);
        //    }
        //}

        //private string _city = "";
        //public string City
        //{
        //    get { return _city; }
        //    set
        //    {
        //        SetPropertyValue(ref _city, value);
        //    }
        //}

        private ValidatableObject<string> _message = null;
        public ValidatableObject<string> Message
        {
            get { return _message; }
            set { SetPropertyValue(ref _message, value); }
        }

        //private bool _duringOffice = false;
        //public bool DuringOffice
        //{
        //    get { return _duringOffice; }
        //    set
        //    {
        //        SetPropertyValue(ref _duringOffice, value);
        //    }
        //}

        //private bool _afterOffice = false;
        //public bool AfterOffice
        //{
        //    get { return _afterOffice; }
        //    set
        //    {
        //        SetPropertyValue(ref _afterOffice, value);
        //    }
        //}


        List<string> _days = new List<string>();

        private bool _mondayChecked = false;
        public bool MondayChecked
        {
            get { return _mondayChecked; }
            set
            {
                SetPropertyValue(ref _mondayChecked, value);
                SetDays("Mon", value);
            }
        }

        private bool _tuesdayChecked = false;
        public bool TuesdayChecked
        {
            get { return _tuesdayChecked; }
            set
            {
                SetPropertyValue(ref _tuesdayChecked, value);
                SetDays("Tue", value);
            }
        }

        private bool _wednesdayChecked = false;
        public bool WednesdayChecked
        {
            get { return _wednesdayChecked; }
            set
            {
                SetPropertyValue(ref _wednesdayChecked, value);
                SetDays("Wed", value);
            }
        }

        private bool _thursdayChecked = false;
        public bool ThursdayChecked
        {
            get { return _thursdayChecked; }
            set
            {
                SetPropertyValue(ref _thursdayChecked, value);
                SetDays("Thur", value);
            }
        }

        private bool _fridayChecked = false;
        public bool FridayChecked
        {
            get { return _fridayChecked; }
            set
            {
                SetPropertyValue(ref _fridayChecked, value);
                SetDays("Fri", value);
            }
        }

        private bool _weekendChecked = false;
        public bool WeekendChecked
        {
            get { return _weekendChecked; }
            set
            {
                SetPropertyValue(ref _weekendChecked, value);
                SetDays("Weekend", value);
            }
        }

        private bool _acClicked = false;
        private bool _anytimeChecked = false;
        public bool AnytimeChecked
        {
            get { return _anytimeChecked; }
            set
            {
                SetPropertyValue(ref _anytimeChecked, value);
                _acClicked = true;
                CheckAll(_anytimeChecked);
                _acClicked = false;
            }
        }

        private void CheckAll(bool val)
        {
            if (!_fromDays)
            {
                MondayChecked = val;
                TuesdayChecked = val;
                WednesdayChecked = val;
                ThursdayChecked = val;
                FridayChecked = val;
                WeekendChecked = val;
            }
        }

        bool _fromDays = false;
        private void SetDays(string day, bool val)
        {
            _fromDays = true;
            if (val)
            {
                if (!_days.Contains(day))
                    _days.Add(day);

                if (!_acClicked)
                    AnytimeChecked = IsAllChecked();
            }
            else
            {
                _days.Remove(day);

                if (!_acClicked)
                    AnytimeChecked = false;
            }
            _fromDays = false;
        }

        private bool IsAllChecked()
        {
            return MondayChecked && TuesdayChecked && WednesdayChecked && ThursdayChecked && FridayChecked && WeekendChecked;
        }


        public Command SetMessageCommand { get; private set; }
        public Command SubmitCommand { get; set; }
        public Command<string> CheckCommand { get; private set; }

        public Command<string> CallCommand { get; private set; }
        public Command<string> SearchCommand { get; private set; }

        private void ExecuteCheckCommand(string value)
        {
            switch (value)
            {
                case "1": //Tuesday ...
                    TuesdayChecked = !TuesdayChecked;
                    break;
                case "2":
                    WednesdayChecked = !WednesdayChecked;
                    break;
                case "3":
                    ThursdayChecked = !ThursdayChecked;
                    break;
                case "4":
                    FridayChecked = !FridayChecked;
                    break;
                case "5":
                    WeekendChecked = !WeekendChecked;
                    break;
                case "6":
                    AnytimeChecked = !AnytimeChecked;
                    break;
                default: //0 Monday
                    MondayChecked = !MondayChecked;
                    break;
            }
        }

        private async void ExecuteSearchCommand(string param)
        {
            if (param == "country")
            {
                Country.ClearError();
                await SearchPage(_countries, "Country", _countryCallBack);
            }
            else//city
            {
                if (!string.IsNullOrEmpty(Country.Value))
                {
                    City.ClearError();
                    //_cities = await _getDataService.GetCities(Country);
                    if (_cities != null && _cities.Count > 0)
                    {
                        await SearchPage(_cities, "City", _cityCallBack);
                    }
                }
            }
        }

        private async Task SearchPage(List<string> toSearch, string title, Action<string> callBack)
        {
            Dictionary<string, object> _params = new Dictionary<string, object>();
            _params.Add("tosearch", toSearch);
            _params.Add("title", title);
            _params.Add("callBack", callBack);
            await NavigationService.NavigateTo("SearchPage", _params);
        }

        Action<string> _countryCallBack = null;
        Action<string> _cityCallBack = null;


        private async void ExecuteSubmit()
        {
            if (ValidBeforeSubmit())
            {
                Appointment appointment = new Appointment()
                {
                    Name = Name.Value,
                    Email = Email.Value,
                    ContactNumber = ContactNumber.Value,
                    Employer = Employer.Value,
                    Country = Country.Value,
                    City = City.Value,
                    Message = Message.Value,
                    PreferredDays = string.Join(',', _days.ToArray()),
                    PreferredTime = "1",//DuringOffice ? "1" : "2",
                    RequestType = "1"
                };
                RequestResponse result = await _workwiseService.SendAppointment(appointment);

                //Test
                //RequestResponse result = new RequestResponse()
                //{
                //    Code = "0001"
                //};
                //Test

                if (result.Code != 0)
                {
                    await ShowAlertAsync("Appointment", "You have successfully sent in an appointment request.\n\nA representative will get back to you as soon as possible.");
                    ClearFields();
                    //await NavigationService.GoBack();
                    MessagingCenter.Send(this, "home");
                }
                else
                {
                    await ShowAlertAsync("Appointment", result.Message);
                }
            }
        }


        private void ClearFields()
        {
            Name.Value = "";
            Email.Value = "";
            ContactNumber.Value = "";
            Employer.Value = "";
            //Location.Value = "";
            Country.Value = "";
            City.Value = "";
            Message.Value = "";

            MondayChecked = false;
            TuesdayChecked = false;
            WednesdayChecked = false;
            ThursdayChecked = false;
            FridayChecked = false;
            WeekendChecked = false;
            AnytimeChecked = false;

            //DuringOffice = false;
            //AfterOffice = false;
        }

        private bool ValidBeforeSubmit()
        {
            bool ret = false;
            string _errMsg = "";

            Name.Validate();
            Email.Validate();
            ContactNumber.Validate();
            Employer.Validate();
            //Location.Validate();
            Country.Validate();
            City.Validate();
            Message.Validate();

            bool _fields = Name.IsValid && Email.IsValid && ContactNumber.IsValid &&
                   Employer.IsValid && Country.IsValid && City.IsValid;

            if (!_fields)
                _errMsg += "Check fields.\n";

            if (_days.Count <= 0)
                _errMsg += "Please select preferred day(s).\n";

            if (!Message.IsValid)
                _errMsg += "Please add message.\n";

            //var time = (DuringOffice || AfterOffice);
            //if (!time)
            //    _errMsg += "Please select preferred time.\n";

            ret = _fields && _days.Count > 0 && Message.IsValid;// && time;

            if (!ret)
                ShowAlert("Appointment", _errMsg);

            return ret;
        }

        //public void CheckMessageLength()
        //{
        //    string messageEntered = Message;

        //    if(string.)
        //}
    }
}
