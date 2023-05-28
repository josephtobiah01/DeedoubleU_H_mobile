using DrakeWorkwise.ViewModels;

namespace DrakeWorkwise.Views;

public partial class MessageEditorPage : ContentPage
{
	public MessageEditorPage(MessageEditorViewModel vm)
	{
        BindingContext = vm;
		InitializeComponent();
    }


	private void Entry_Focused(object sender, FocusEventArgs e)
	{
        if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
        {
            //layout.TranslateTo(0, -300, 50);
            //layout.HeightRequest = currHeight - 100;
            space.HeightRequest = 350;
        }
    }

	private void Entry_Unfocused(object sender, FocusEventArgs e)
	{
        if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
        {
            //layout.TranslateTo(0, 0, 50);
            //layout.HeightRequest = currHeight;
            space.HeightRequest = 0;
        }
    }
}