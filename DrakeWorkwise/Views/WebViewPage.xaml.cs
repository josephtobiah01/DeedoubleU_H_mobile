using DrakeWorkwise.Interfaces;
using DrakeWorkwise.ViewModels;

namespace DrakeWorkwise.Views;

public partial class WebViewPage : ContentPage
{
	public WebViewPage(WebViewPageViewModel webViewPageViewModel)
	{
		this.BindingContext = webViewPageViewModel;
		InitializeComponent();
	}

	private void webView_Navigating(object sender, WebNavigatingEventArgs e)
	{
		if (e.Url.Contains("tel"))
		{
			var phone = (IPhoneService)BindingContext;
			phone.PhoneCall(e.Url.Replace("tel:", ""));
			e.Cancel = true;
		}
	}

}