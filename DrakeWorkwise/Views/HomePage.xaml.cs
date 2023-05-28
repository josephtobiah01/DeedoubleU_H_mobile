using DrakeWorkwise.ViewModels;


namespace DrakeWorkwise.Views;



public partial class HomePage : ContentPage
{
	public HomePage(HomePageViewModel viewModel)
	{
        BindingContext = viewModel;
        InitializeComponent();
		
	}
}