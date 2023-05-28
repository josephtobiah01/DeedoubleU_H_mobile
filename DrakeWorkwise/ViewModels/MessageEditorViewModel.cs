using DrakeWorkwise.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeWorkwise.ViewModels
{
    public class MessageEditorViewModel : ViewModelBase
    {
        public MessageEditorViewModel(INavigationService nav) : base(nav)
        {
            ButtonCommand = new Command(async () =>
            {
                MessagingCenter.Send(this, _from);

                await NavigationService.GoBack();
            });
        }

        private string _value = "";
        public string Value
        {
            get { return _value; }
            set
            {
                SetPropertyValue(ref _value, value);
            }
        }

        public Command ButtonCommand { get; private set; }

        private string _from = "";
        public override Task OnNavigatingTo(Dictionary<string, object> parameter)
        {
            if(parameter.TryGetValue("from", out object from))
            {
                _from = from.ToString();
            }

            if(parameter.TryGetValue("message", out object message))
            {
                Value = message?.ToString() ?? "";
            }

            if (parameter.TryGetValue("comments", out object comments))
            {
                Value = comments?.ToString() ?? "";
            }

            return base.OnNavigatingTo(parameter);
        }
    }
}
