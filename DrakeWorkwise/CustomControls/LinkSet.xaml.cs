using System.Windows.Input;

namespace DrakeWorkwise.CustomControls;

public partial class LinkSet : ContentView
{
	public LinkSet()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty LinkCommandProperty =
        BindableProperty.Create("LinkCommand", typeof(Command<string>), typeof(LinkSet));

    public Command<string> LinkCommand
    {
        get { return (Command<string>)GetValue(LinkCommandProperty); }
        set { SetValue(LinkCommandProperty, value); }
    }
}