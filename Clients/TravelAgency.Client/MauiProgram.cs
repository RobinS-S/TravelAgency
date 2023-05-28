using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Nager.Country;
using Nager.Country.Translation;
using SkiaSharp.Views.Maui.Controls.Hosting;
using TravelAgency.Client.Auth.Http;
using TravelAgency.Client.Auth.Pages;
using TravelAgency.Client.Auth.Services;
using TravelAgency.Client.Pages;
using TravelAgency.Client.Pages.Account;
using TravelAgency.Client.Pages.Countries;
using TravelAgency.Client.Pages.Countries.Detail;
using TravelAgency.Client.Pages.Locations;
using TravelAgency.Client.Pages.Locations.Detail;
using TravelAgency.Client.Pages.Main;
using TravelAgency.Client.Pages.Residences;
using TravelAgency.Client.Pages.Residences.Detail;
using TravelAgency.Client.Repositories;
using TravelAgency.Client.Services;

namespace TravelAgency.Client
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
#if DEBUG
            .UseMauiCommunityToolkit()
#else
            .UseMauiCommunityToolkit(options =>
        {
	        options.SetShouldSuppressExceptionsInAnimations(true);
	        options.SetShouldSuppressExceptionsInBehaviors(true);
	        options.SetShouldSuppressExceptionsInConverters(true);
        })
#endif
                .UseSkiaSharp(true)
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

            // Geolocation
            builder.Services.AddSingleton(Geolocation.Default);
            builder.Services.AddSingleton<GeolocationService>();

            // Other services
            builder.Services.AddSingleton<TranslationProvider>();
            builder.Services.AddSingleton<CountryProvider>();

            // Theming
            builder.Services.AddSingleton<SettingsService>();

            // Repositories (that call APIs)
            builder.Services.AddSingleton<CountryRepository>();
            builder.Services.AddSingleton<LocationRepository>();
            builder.Services.AddSingleton<ResidenceRepository>();

            // Add all pages and viewmodels here for dependency injection to work
            builder.Services.AddTransient<AuthenticatedContentPage>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<CountriesPage>();
            builder.Services.AddTransient<CountriesPageViewModel>();
            builder.Services.AddTransient<LocationsPage>();
            builder.Services.AddTransient<LocationsPageViewModel>();
            builder.Services.AddTransient<LocationDetailPage>();
            builder.Services.AddTransient<LocationDetailPageViewModel>();
            builder.Services.AddTransient<CountryDetailPage>();
            builder.Services.AddTransient<CountryDetailPageViewModel>();
            builder.Services.AddTransient<ResidencesPage>();
            builder.Services.AddTransient<ResidencesPageViewModel>();
            builder.Services.AddTransient<ResidenceDetailPage>();
            builder.Services.AddTransient<ResidenceDetailPageViewModel>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LogoutPage>();
            builder.Services.AddTransient<AccountPage>();
            builder.Services.AddTransient<AppShell>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}