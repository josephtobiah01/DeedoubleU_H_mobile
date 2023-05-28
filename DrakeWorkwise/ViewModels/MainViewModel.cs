using DrakeWorkwise.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DrakeWorkwise.ViewModels
{
    public class MainViewModel : CallUsViewModelBase
    {
        public MainViewModel(INavigationService nav) : base(nav)
        {
            Title = "Main page testing only.";
            NavigateToHome = new Command(async () =>
            {
                var param = new Dictionary<string, object>();
                param.Add("testing", "testingonly");
                //await NavigateTo("HomePage", param);
            });
        }

        public ICommand NavigateToHome { get; set; }
        
    }
}
