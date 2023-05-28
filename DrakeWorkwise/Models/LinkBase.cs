using DrakeWorkwise.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DrakeWorkwise.Models
{
    public class LinkBase : BindableBase
    {
        private string _image = "";
        public string Image
        {
            get { return _image; }
            set { SetPropertyValue(ref _image, value); }
        }

        private string _text = "";
        public string Text
        {
            get { return _text; }
            set { SetPropertyValue(ref _text, value); }
        }

        //private Command<string> _command = null;
        //public Command<string> Command
        //{
        //    get { return _command; }
        //    set { SetPropertyValue(ref _command, value); }
        //}

        private string _linkParameter = "";
        public string LinkParameter
        {
            get { return _linkParameter; }
            set
            {
                SetPropertyValue(ref _linkParameter, value);
            }
        }
    }
}
