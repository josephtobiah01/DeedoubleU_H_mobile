using DrakeWorkwise.ViewModels;

namespace DrakeWorkwise.Views;

public partial class AppointmentPage : ContentPage
{
	public AppointmentPage(AppointmentViewModel appointmentViewModel)
	{
		this.BindingContext = appointmentViewModel;
		InitializeComponent();
        //editMessage.EditorFocusAction = new Action<bool>((focused) =>
        //{
        //    if (focused)
        //    {
        //        if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
        //        {
        //            layout.TranslateTo(0, -300, 50);
        //        }
        //    }
        //    else
        //    {
        //        if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
        //        {
        //            layout.TranslateTo(0, 0, 50);
        //        }
        //    }
        //});
	}
}