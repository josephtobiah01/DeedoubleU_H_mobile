using DrakeWorkwise.ViewModels;


namespace DrakeWorkwise.Views;

public partial class CrisisContactPage : ContentPage
{
	public CrisisContactPage(CrisisContactViewModel viewModel)
	{
        BindingContext = viewModel;
        InitializeComponent();
		
	}
}