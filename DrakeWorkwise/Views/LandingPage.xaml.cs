using DrakeWorkwise.Models;
using DrakeWorkwise.ViewModels;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;

namespace DrakeWorkwise.Views;

public partial class LandingPage : Microsoft.Maui.Controls.TabbedPage
{
	public LandingPage(HomePage homepage, WebViewPage eap, AppointmentPage appointment, SettingsPage settings, LandingPageViewModel landingPageViewModel)
	{
		this.BindingContext = landingPageViewModel;


        InitializeComponent();
		this.Children.Add(homepage);
		this.Children.Add(appointment);
		this.Children.Add(eap);
		this.Children.Add(settings);

        On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

        MessagingCenter.Subscribe<HomePageViewModel>(this, "appointments", (sender) =>
			{
				this.CurrentPage = this.Children[1];
			});

        MessagingCenter.Subscribe<AppointmentViewModel>(this, "home", (sender) =>
        {
            this.CurrentPage = this.Children[0];
        });
    }

    ~LandingPage()
	{
		MessagingCenter.Unsubscribe<HomePageViewModel>(this, "appointments");

		MessagingCenter.Unsubscribe<AppointmentViewModel>(this, "home");
    }

	private void TabbedPage_CurrentPageChanged(object sender, EventArgs e)
	{
		titleBar.TitleText = CurrentPage.Title == "Home" ? this.Title : CurrentPage.Title;
	}
}