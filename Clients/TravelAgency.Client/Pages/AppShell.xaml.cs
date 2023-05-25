using TravelAgency.Client.Auth.Services;
using TravelAgency.Client.Pages.Countries.Detail;
using TravelAgency.Client.Services;

namespace TravelAgency.Client.Pages
{
    public partial class AppShell : Shell
    {
        private AuthService _authService;
        private SettingsService _settingsService;

        public AppShell()
        {
            InitializeComponent();
            _authService = ServiceProviderHelper.GetService<AuthService>()!;
            _settingsService = ServiceProviderHelper.GetService<SettingsService>()!;
            BindingContext = _settingsService;

            Routing.RegisterRoute(nameof(CountryDetailPage), typeof(CountryDetailPage));
        }
    }
}