using DrakeWorkwise.Interfaces;
using DrakeWorkwise.Models;
using DrakeWorkwise.Services;
using DrakeWorkwise.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeWorkwise.ViewModels
{
    public class ContactUsViewModel : CallUsViewModelBase
    {
        private readonly IWorkwiseService _workwiseService;
        private readonly IPhoneService _phoneService;

        public ContactUsViewModel(INavigationService nav, IWorkwiseService workwiseService, IPhoneService phoneService) : base(nav)
        {
            _workwiseService = workwiseService;
            _phoneService = phoneService;
            Initialize();
        }

        ~ContactUsViewModel()
        {
            MessagingCenter.Unsubscribe<MessageEditorViewModel>(this, "contactus");
        }

        private void Initialize()
        {
            IsContact = true;
            SubmitCommand = new Command(ExecuteSubmit);
            SetMessageCommand = new Command(async () =>
            {
                Comments.ClearError();
                Dictionary<string, object> _param = new Dictionary<string, object>();
                _param.Add("from", "contactus");
                _param.Add("comments", Comments.Value);
                await NavigationService.NavigateTo(nameof(Views.MessageEditorPage), _param);
            });

            MessagingCenter.Subscribe<MessageEditorViewModel>(this, "contactus", (vm) =>
            {
                Comments.Value = vm.Value;
                Comments.Validate();
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


            Name = new ValidatableObject<string>() { Title = "Name", Placeholder = "Name" };
            Name.Rules.Add(new ValidNameValidationRule() { ValidationMessage = "Invalid name." });
            Name.Rules.Add(new TextLimitValidationRule() { MaxChar = 200 });
            Name.Rules.Add(new EmptyOrNullValidationRule() { ValidationMessage = "Name must not be empty." });

            Email = new ValidatableObject<string>() { Title = "Email", Placeholder = "email@mail.com", KeyBoard = Keyboard.Email };
            Email.Rules.Add(new EmailValidationRule() { ValidationMessage = "Invalid email." });
            Email.Rules.Add(new EmptyOrNullValidationRule() { ValidationMessage = "Email must not be empty." });

            Phone = new ValidatableObject<string>() { Title = "Contact Number", Placeholder = "Contact Number", KeyBoard = Keyboard.Telephone };
            //ContactNumber.Rules.Add(new PhoneNumberValidation() { ValidationMessage = "Invalid contact number." });

            Comments = new ValidatableObject<string>() { Placeholder = "Message" };
            //Message.Rules.Add(new AlphanumericValidation() { ValidationMessage = "Invalid message." });
            Comments.Rules.Add(new TextLimitValidationRule() { MaxChar = 500 });
            Comments.Rules.Add(new EmptyOrNullValidationRule() { ValidationMessage = "Message must not be empty." });
        }


        private bool _isContact = true;
        public bool IsContact
        {
            get { return _isContact; }
            set
            {
                SetPropertyValue(ref _isContact, value);
                Title = _isContact ? "Contact Us" : "Send feedback";
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

        private ValidatableObject<string> _phone = null;
        public ValidatableObject<string> Phone
        {
            get { return _phone; }
            set { SetPropertyValue(ref _phone, value); }
        }

        private ValidatableObject<string> _comments = null;
        public ValidatableObject<string> Comments
        {
            get { return _comments; }
            set { SetPropertyValue(ref _comments, value); }
        }

        private bool _isEmployee = false;
        public bool IsEmployee
        {
            get { return _isEmployee; }
            set
            {
                SetPropertyValue(ref _isEmployee, value);
            }
        }

        private bool _isEmployer = false;
        public bool IsEmployer
        {
            get { return _isEmployer; }
            set
            {
                SetPropertyValue(ref _isEmployer, value);
            }
        }

        private bool _isProvider = false;

        public bool IsProvider
        {
            get { return _isProvider; }
            set
            {
                SetPropertyValue(ref _isProvider, value);
            }
        }

        public override Task OnNavigatingTo(Dictionary<string, object> parameter)
        {
            if (parameter != null)
            {
                if (parameter.TryGetValue("IsContact", out object isCon))
                {
                    IsContact = (bool)isCon;
                }
            }
            return base.OnNavigatingTo(parameter);
        }

        public Command SubmitCommand { get; set; }
        public Command SetMessageCommand { get; private set; }

        public Command<string> CallCommand { get; private set; }

        private async void ExecuteSubmit()
        {
            if (ValidBeforeSubmit())
            {
                ContactUs contactUs = new ContactUs()
                {
                    Name = Name.Value,
                    Email = Email.Value,
                    Phone = Phone.Value,
                    Comments = Comments.Value,
                    Position = ContactType()
                };
                RequestResponse result = await _workwiseService.SendContactUs(contactUs, IsContact);
                string msg = "";
                if (result.Code != 0)
                {
                    msg = IsContact ?
                        "You have successfully sent in a general enquiry.\n\nA representative will get back to you as soon as possible."
                        : "Thank you for your feedback.\n\nYour comments are incredibly valued, thank you for helping us improve this app.";

                }
                else
                {
                    msg = IsContact ?
                        "Unable to send the request, please try again later." :
                        "Unable to send feedback, please try again later.";
                }

                await ShowAlertAsync(Title, msg);

                await NavigationService.GoBack();
            }
        }

        private string ContactType()
        {
            return IsEmployer ? "Employer" : IsEmployee ? "Employee" : IsProvider ? "Provider" : "None";
        }

        private bool ValidBeforeSubmit()
        {
            bool ret = false;
            string _errMsg = "";

            Name.Validate();
            Email.Validate();
            Phone.Validate();
            Comments.Validate();

            bool _fields = Name.IsValid && Email.IsValid; /*&& ContactNumber.IsValid;*/

            if (!_fields)
                _errMsg += "Check fields.\n";

            if (!Comments.IsValid)
                _errMsg += "Please add message.\n";

            var youAre = (IsEmployee || IsEmployer || IsProvider);
            if (!youAre)
                _errMsg += "Please select a user type.\n";

            ret = _fields && Comments.IsValid && youAre;

            if (!ret)
                ShowAlert(Title, _errMsg);

            return ret;
        }
    }
}
