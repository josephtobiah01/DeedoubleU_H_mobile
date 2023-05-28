namespace DrakeWorkwise.CustomControls;

public partial class TitleButton : ContentView
{
	public TitleButton()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty TitleTextProperty =
BindableProperty.Create("TitleText", typeof(string), typeof(TitleButton));

    public string TitleText
    {
        get { return (string)GetValue(TitleTextProperty); }
        set { SetValue(TitleTextProperty, value); }
    }

    public static readonly BindableProperty ButtonTextProperty =
BindableProperty.Create("ButtonText", typeof(string), typeof(TitleButton));

    public string ButtonText
    {
        get { return (string)GetValue(ButtonTextProperty); }
        set { SetValue(ButtonTextProperty, value); }
    }
}