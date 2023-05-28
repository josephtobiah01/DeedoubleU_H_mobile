namespace DrakeWorkwise.CustomControls;

public partial class TitleBar : ContentView
{
	public TitleBar()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty HasPhoneLinkProperty =
BindableProperty.Create("HasPhoneLink", typeof(bool), typeof(TitleBar));

    public bool HasPhoneLink
    {
        get { return (bool)GetValue(HasPhoneLinkProperty); }
        set { SetValue(HasPhoneLinkProperty, value); }
    }

    public static readonly BindableProperty HasSettingsLinkProperty =
BindableProperty.Create("HasSettingsLink", typeof(bool), typeof(TitleBar));

    public bool HasSettingsLink
    {
        get { return (bool)GetValue(HasSettingsLinkProperty); }
        set { SetValue(HasSettingsLinkProperty, value); }
    }

    public static readonly BindableProperty TitleTextProperty =
BindableProperty.Create("TitleText", typeof(string), typeof(TitleBar));

    public string TitleText
    {
        get { return (string)GetValue(TitleTextProperty); }
        set { SetValue(TitleTextProperty, value); }
    }

//    public static readonly BindableProperty LinkCommandProperty =
//BindableProperty.Create("LinkCommand", typeof(Command<string>), typeof(TitleBar));

//    public Command<string> LinkCommand
//    {
//        get { return (Command<string>)GetValue(LinkCommandProperty); }
//        set { SetValue(LinkCommandProperty, value); }
//    }

}