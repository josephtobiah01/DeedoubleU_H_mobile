using DrakeWorkwise.ViewModels;
//using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using System.Net;

namespace DrakeWorkwise.Views;

public partial class WellbeingHubPage : ContentPage
{
    public WellbeingHubPage(WellbeingHubPageViewModel wellbeingHubPageViewModel)
    {
        this.BindingContext = wellbeingHubPageViewModel;

        InitializeComponent();
        if (((WellbeingHubPageViewModel)BindingContext).Cookie != null)
        {
            webView.Cookies = ((WellbeingHubPageViewModel)BindingContext).Cookie;
        }


        Microsoft.Maui.Handlers.WebViewHandler.Mapper.AppendToMapping("MyCustomization", (handler, view) =>
        {
#if ANDROID
            if (view is WebView)  
            {  
                Android.Webkit.WebView wv = handler.PlatformView;  
                wv.Settings.JavaScriptEnabled = true;  
                wv.Settings.AllowFileAccess = true;
                wv.Settings.AllowContentAccess= true;
                wv.Settings.AllowFileAccessFromFileURLs = true;
                wv.Settings.AllowUniversalAccessFromFileURLs = true;
                wv.Settings.PluginsEnabled = true;
            }  
            
#endif
        });

        webView.Navigating += (o, e) =>
        {
            if (!_initial)
            {
                ((WellbeingHubPageViewModel)BindingContext).Cookie = webView.Cookies;
                if (e.Url.Contains("/portal"))
                {
                    ((WellbeingHubPageViewModel)BindingContext).URL = e.Url;
                }
                //((WellbeingHubPageViewModel)BindingContext).URL = e.Url;      
            }
            _initial = false;
        };

    }

    bool _initial = true;
}