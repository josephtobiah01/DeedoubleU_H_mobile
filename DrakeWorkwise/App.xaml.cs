using DrakeWorkwise.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace DrakeWorkwise;

public partial class App : Application
{
	public App(LandingPage landingpage, HomePage homepage)
	{
        AppCenter.Start("9443e9bf-bbfe-478e-b842-49d0ee7b6050",
                   typeof(Analytics), typeof(Crashes));
        InitializeComponent();
#if ANDROID
        MainPage = new AppShell();
#else
        MainPage = new NavigationPage(landingpage);
#endif

        //MainPage = new NavigationPage(landingpage);


        //MainPage = new NavigationPage(new Views.LandingPage(new Views.HomePage(new ViewModels.HomePageViewModel(new PhoneService())), new Views.EAPPrivacyPage(), new Views.AppointmentPage(), new Views.SettingsPage())); //new AppShell();
    }




}
