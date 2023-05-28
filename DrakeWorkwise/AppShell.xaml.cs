using DrakeWorkwise.ViewModels;
using DrakeWorkwise.Views;

namespace DrakeWorkwise;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        //Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(AppointmentPage), typeof(AppointmentPage));
        Routing.RegisterRoute(nameof(CallUsPage), typeof(CallUsPage));
        Routing.RegisterRoute(nameof(ContactUs), typeof(ContactUs));
        Routing.RegisterRoute(nameof(CrisisContactPage), typeof(CrisisContactPage));
        Routing.RegisterRoute(nameof(EAPPrivacyPage), typeof(EAPPrivacyPage));
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(ResourcesPage), typeof(ResourcesPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        Routing.RegisterRoute(nameof(WebViewPage), typeof(WebViewPage));
        Routing.RegisterRoute(nameof(MessageEditorPage), typeof(MessageEditorPage));
        Routing.RegisterRoute(nameof(WellbeingHubPage), typeof(WellbeingHubPage));

        MessagingCenter.Subscribe<HomePageViewModel>(this, "appointments", (sender) =>
        {
            this.CurrentItem = appointments;
        });

        MessagingCenter.Subscribe<AppointmentViewModel>(this, "home", (sender) =>
        {
            this.CurrentItem = home;
        });

    }

    ~AppShell()
    {
        MessagingCenter.Unsubscribe<HomePageViewModel>(this, "appointments");
        MessagingCenter.Unsubscribe<AppointmentViewModel>(this, "home");
    }

	
}
