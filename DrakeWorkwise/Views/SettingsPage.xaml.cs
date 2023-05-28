using DrakeWorkwise.ViewModels;

namespace DrakeWorkwise.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsPageViewModel settingsPageViewModel)
	{
		this.BindingContext = settingsPageViewModel;
		InitializeComponent();
	}
}