using DrakeWorkwise.ViewModels;

namespace DrakeWorkwise.Views;

public partial class CallUsPage : ContentPage
{
	public CallUsPage(CallUsViewModel callUsViewModel)
	{
		BindingContext = callUsViewModel;
		InitializeComponent();
	}
}