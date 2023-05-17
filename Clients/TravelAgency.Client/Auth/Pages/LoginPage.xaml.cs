using CommunityToolkit.Maui.Alerts;
using TravelAgency.Client.Auth.Services;

namespace TravelAgency.Client.Auth.Pages;

public partial class LoginPage
{
    private readonly AuthService _authService;

    public LoginPage(AuthService authService)
    {
        _authService = authService;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;

        try
        {
            var hasToken = await _authService.LoadTokenFromStorage();
            if (!hasToken || !await _authService.TestLogin())
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
                    await this.DisplaySnackbar("LOGIN ERROR: " + loginResult.Error, null, "OK", TimeSpan.FromSeconds(5));
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
            await this.DisplaySnackbar("Login was cancelled.", null, "OK", TimeSpan.FromSeconds(3));
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