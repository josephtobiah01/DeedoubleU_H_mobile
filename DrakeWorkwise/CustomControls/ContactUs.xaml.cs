namespace DrakeWorkwise.CustomControls;

public partial class ContactUs : ContentView
{
	public ContactUs()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty LinkCommandProperty =
BindableProperty.Create("LinkCommand", typeof(Command<string>), typeof(CrisisContact));

    public Command<string> LinkCommand
    {
        get { return (Command<string>)GetValue(LinkCommandProperty); }
        set { SetValue(LinkCommandProperty, value); }
    }
}