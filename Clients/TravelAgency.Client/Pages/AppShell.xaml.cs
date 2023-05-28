using TravelAgency.Client.Auth.Services;
using TravelAgency.Client.Pages.Countries.Detail;
using TravelAgency.Client.Pages.Locations;
using TravelAgency.Client.Pages.Locations.Detail;
using TravelAgency.Client.Pages.Residences;
using TravelAgency.Client.Pages.Residences.Detail;
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
            Routing.RegisterRoute(nameof(LocationsPage), typeof(LocationsPage));
            Routing.RegisterRoute(nameof(ResidencesPage), typeof(ResidencesPage));
            Routing.RegisterRoute(nameof(LocationDetailPage), typeof(LocationDetailPage));
            Routing.RegisterRoute(nameof(ResidenceDetailPage), typeof(ResidenceDetailPage));
        }
    }
}