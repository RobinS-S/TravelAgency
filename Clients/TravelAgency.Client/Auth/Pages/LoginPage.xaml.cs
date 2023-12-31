using CommunityToolkit.Maui.Alerts;
using TravelAgency.Client.Auth.Services;
using TravelAgency.Client.Resources.Localization;

namespace TravelAgency.Client.Auth.Pages;

public partial class LoginPage
{
    private readonly AuthService _authService;
    private readonly HttpService _httpService;

    public LoginPage(AuthService authService, HttpService httpService)
    {
        _authService = authService;
        _httpService = httpService;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;

        try
        {
            var hasToken = await _authService.LoadTokenFromStorage();
            if (!hasToken || !await _httpService.TestLogin())
            {
                var browser =
#if WINDOWS
                    new Platforms.Windows.Auth.WebViewBrowserAuthenticatorBrowser(WebViewInstance); // Uses a WebView to avoid known issue: https://github.com/dotnet/maui/issues/2702
#else
                    new TravelAgency.Client.Auth.Browser(); // Implementation for iOS (Catalyst) and Android
#endif
                var loginResult = await _authService.Login(browser);

                await _authService.SetAuthResult(loginResult);

                if (!loginResult.IsError)
                {
                    Dispatcher.Dispatch(NavigateToDefaultAuthenticatedPage);
                }

#if DEBUG
                else
                {
                    await this.DisplaySnackbar($"{Translations.Error}: {loginResult.Error}", null, "OK", TimeSpan.FromSeconds(5));
                }
#endif
#if WINDOWS
                browser.Unsubscribe();
#endif
            }
            else
            {
                Dispatcher.Dispatch(NavigateToDefaultAuthenticatedPage);
            }
        }
        catch (TaskCanceledException)
        {
            await this.DisplaySnackbar(Translations.LoginCancelled, null, "OK", TimeSpan.FromSeconds(3));
            await Shell.Current.Navigation.PopAsync(true);
        }
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
    }

    private static async void NavigateToDefaultAuthenticatedPage()
    {
        await Shell.Current.GoToAsync(ApiConfig.AppShellDefaultPageAfterLogin);
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
    }
}