using DrakeWorkwise.Services;
using DrakeWorkwise.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using DrakeWorkwise.Views;
using DrakeWorkwise.ViewModels;
using Microsoft.Maui.Platform;

namespace DrakeWorkwise;
public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		RegisterNavigation();

        SetupServices(builder.Services);

        builder.Services.AddTransient<CrisisContactPage>();
		builder.Services.AddTransient<CrisisContactViewModel>();
	
		builder.Services.AddTransient<HomePage>();
		builder.Services.AddTransient<HomePageViewModel>();

        builder.Services.AddTransient<CallUsPage>();
        builder.Services.AddTransient<CallUsViewModel>();

        builder.Services.AddTransient<ContactUs>();
        builder.Services.AddTransient<ContactUsViewModel>();

        builder.Services.AddTransient<AppointmentPage>();
        builder.Services.AddTransient<AppointmentViewModel>();

        builder.Services.AddTransient<WebViewPage>();
        builder.Services.AddTransient<WebViewPageViewModel>();

        builder.Services.AddTransient<ResourcesPage>();
        builder.Services.AddTransient<ResourcesPageViewModel>();

        builder.Services.AddTransient<EAPPrivacyPage>();

        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<SettingsPageViewModel>();


        builder.Services.AddSingleton<LandingPage>();
        builder.Services.AddTransient<LandingPageViewModel>();

        builder.Services.AddTransient<SearchPage>();
        builder.Services.AddTransient<SearchPageViewModel>();

        builder.Services.AddTransient<MessageEditorPage>();
        builder.Services.AddTransient<MessageEditorViewModel>();

        builder.Services.AddTransient<WellbeingHubPage>();
        builder.Services.AddTransient<WellbeingHubPageViewModel>();


#if IOS     // add handler in MauiProgram.cs   
        Microsoft.Maui.Handlers.ScrollViewHandler.Mapper.AppendToMapping("custom", (handler, view) => {
            handler.PlatformView.UpdateContentSize(handler.VirtualView.ContentSize); handler.PlatformArrange(handler.PlatformView.Frame.ToRectangle());
        });
#endif

        return builder.Build();
	}

	private static void SetupServices(IServiceCollection services)
	{
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddScoped<IPhoneService, PhoneService>();

		var apiManager = new ApiManager();
		services.AddSingleton<IApiManager>(apiManager);

        services.AddScoped<IWorkwiseService, WorkwiseService>();

        var getData = new GetDataService();
        services.AddSingleton<IGetDataService>(getData);
    }

	private static void RegisterNavigation()
	{
		RegisterForNavigation.Register<HomePage>();
        RegisterForNavigation.Register<AppointmentPage>();
        RegisterForNavigation.Register<CallUsPage>();
        RegisterForNavigation.Register<ContactUs>();
        RegisterForNavigation.Register<CrisisContactPage>();
        RegisterForNavigation.Register<EAPPrivacyPage>();
        RegisterForNavigation.Register<LandingPage>();
        RegisterForNavigation.Register<SettingsPage>();
        RegisterForNavigation.Register<WebViewPage>();
        RegisterForNavigation.Register<ResourcesPage>();
        RegisterForNavigation.Register<SearchPage>();
        RegisterForNavigation.Register<MessageEditorPage>();
        RegisterForNavigation.Register<WellbeingHubPage>();
    }
}
