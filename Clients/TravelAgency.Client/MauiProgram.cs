using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using TravelAgency.Client.Auth.Http;
using TravelAgency.Client.Auth.Pages;
using TravelAgency.Client.Auth.Services;
using TravelAgency.Client.Pages;
using TravelAgency.Client.Pages.Main;
using TravelAgency.Client.Pages.Test;

namespace TravelAgency.Client
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton(new HttpClient(
                #if DEBUG
                    new NonSecureHttpMessageHandler()) // This ignores HTTPS certificate validation errors, for example self-signed ones
                #endif
            );

            // Authentication and API services
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<HttpService>();

            // Add all pages here for dependency injection to work
            builder.Services.AddTransient<AuthenticatedContentPage>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LogoutPage>();
            builder.Services.AddTransient<TestPage>();
            builder.Services.AddTransient<AppShell>();

#if DEBUG
            builder.Logging.AddDebug();
        #endif

            return builder.Build();
        }
    }
}