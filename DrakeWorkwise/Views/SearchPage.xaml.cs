using DrakeWorkwise.ViewModels;

namespace DrakeWorkwise.Views;

public partial class SearchPage : ContentPage
{
	public SearchPage(SearchPageViewModel searchPageViewModel)
	{
		this.BindingContext = searchPageViewModel;
		InitializeComponent();
	}
}