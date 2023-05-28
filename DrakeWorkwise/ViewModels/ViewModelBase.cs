using DrakeWorkwise.Helpers;
using DrakeWorkwise.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DrakeWorkwise.ViewModels
{
    public class ViewModelBase : BindableBase, IQueryAttributable
    {
        public readonly INavigationService NavigationService;


        private string _title = "";
        public string Title
        {
            get { return _title; }
            set { SetPropertyValue(ref _title, value); }
        }


        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual void ApplyQueryAttributes(IDictionary<string, object> query)
        {

        }

        public Task ShowAlertAsync(string title, string message, string cancel = "OK")
        {
            return Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public Task<bool> ShowConfirmationAsync(string title, string message, string accept = "Yes", string cancel = "No")
        {
            return Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        public Task<string> ShowActionSheetAsync(string title, string cancel, string destruction, params string[] buttons)
        {
            return Application.Current.MainPage.DisplayActionSheet(title, cancel, destruction, buttons);
        }

        // ----- "Fire and forget" calls -----

        /// <summary>
        /// "Fire and forget". Method returns BEFORE showing alert.
        /// </summary>
        public void ShowAlert(string title, string message, string cancel = "OK")
        {
            Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                await ShowAlertAsync(title, message, cancel)
            );
        }

        /// <summary>
        /// "Fire and forget". Method returns BEFORE showing alert.
        /// </summary>
        /// <param name="callback">Action to perform afterwards.</param>
        public void ShowConfirmation(string title, string message, Action<bool> callback,
                                     string accept = "Yes", string cancel = "No")
        {
            Application.Current.MainPage.Dispatcher.Dispatch(async () =>
            {
                bool answer = await ShowConfirmationAsync(title, message, accept, cancel);
                callback(answer);
            });
        }

        //public async Task NavigateTo(string name, Dictionary<string, object> parameters = null)
        //{
        //    try
        //    {
        //        //if (parameters == null)
        //        //    await Shell.Current.GoToAsync(name);
        //        //else
        //        //    await Shell.Current.GoToAsync(name, parameters);
        //        if(parameters == null)
        //        {

        //        }
        //        else
        //        {

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        public virtual Task OnNavigatingTo(Dictionary<string, object> parameter)
           => Task.CompletedTask;

        public virtual Task OnNavigatedFrom(bool isForwardNavigation)
            => Task.CompletedTask;

        public virtual Task OnNavigatedTo()
            => Task.CompletedTask;

        public T DecodeParameter<T>(string key, IDictionary<string, object> query)
        {
            //return (T)HttpUtility.UrlDecode(query[key]);
            try
            {
                return (T)query[key];
            }
            catch { return default; }
        }
    }

    public class CallUsViewModelBase : ViewModelBase
    {
        public CallUsViewModelBase(INavigationService navigationService) : base(navigationService)
        {
            CallUsCommand = new Command(async () =>
            {
                await NavigationService.NavigateTo("CallUsPage");
            });

            SettingsCommand = new Command(async () =>
            {
                await NavigationService.NavigateTo("SettingsPage");
            });
        }

        public Command CallUsCommand { get; set; }

        public Command SettingsCommand { get; set; }
    }
}
