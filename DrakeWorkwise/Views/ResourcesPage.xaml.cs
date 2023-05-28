using DrakeWorkwise.ViewModels;


namespace DrakeWorkwise.Views;

public partial class ResourcesPage : ContentPage
{
	public ResourcesPage(ResourcesPageViewModel resourcesPageViewModel)
	{
		this.BindingContext = resourcesPageViewModel;
		InitializeComponent();
    }

    private double width;
    private double height;

    protected override void OnSizeAllocated(double width, double height)
	{
		base.OnSizeAllocated(width, height);

		if (width != this.width || height != this.height)
		{
			this.width = width;
			this.height = height;
			if (width > height)
			{

			}
		}
	}


}