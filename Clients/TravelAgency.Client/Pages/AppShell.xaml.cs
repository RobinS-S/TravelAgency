using TravelAgency.Client.Auth.Services;
using TravelAgency.Client.Pages.Countries.Detail;
using TravelAgency.Client.Services;

namespace TravelAgency.Client.Pages
{
    public partial class AppShell : Shell
    {
        private AuthService _authService;
        private ThemeService _themeService;

        public AppShell()
        {
            InitializeComponent();
            _authService = ServiceProviderHelper.GetService<AuthService>()!;
            _themeService = ServiceProviderHelper.GetService<ThemeService>()!;

            Routing.RegisterRoute(nameof(CountryDetailPage), typeof(CountryDetailPage));
            BindingContext = _themeService;
        }
    }
}