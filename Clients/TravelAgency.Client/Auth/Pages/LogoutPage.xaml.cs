using CommunityToolkit.Maui.Alerts;
using TravelAgency.Client.Auth.Services;

namespace TravelAgency.Client.Auth.Pages;

public partial class LogoutPage
{
    private readonly AuthService _authService;

    public LogoutPage(AuthService authService)
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
            if (hasToken || await _authService.TestLogin())
            {
                var logoutResult = await _authService.Logout(
#if WINDOWS
                    new Platforms.Windows.Auth.WebViewBrowserAuthenticatorBrowser(WebViewInstance) // Uses a WebView to avoid known issue: https://github.com/dotnet/maui/issues/2702
#else
                    new TravelAgency.Client.Auth.Browser() // Implementation for iOS (Catalyst) and Android
#endif
                );

                if (!logoutResult.IsError)
                {
                    Dispatcher.Dispatch(NavigateToDefaultAnonymousPage);
                }

#if DEBUG
                else
                {
                    await this.DisplaySnackbar("LOGOUT ERROR: " + logoutResult.Error, null, "OK", TimeSpan.FromSeconds(5));
                }
#endif
            }
            else
            {
                Dispatcher.Dispatch(NavigateToDefaultAnonymousPage);
            }
        }
        catch (TaskCanceledException)
        {
            await this.DisplaySnackbar("Logout was cancelled.", null, "OK", TimeSpan.FromSeconds(3));
            await Shell.Current.Navigation.PopAsync(true);
        }
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
    }

    private static async void NavigateToDefaultAnonymousPage()
    {
        await Shell.Current.GoToAsync(ApiConfig.AppShellDefaultPageAnonymous);
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
    }
}