using TravelAgency.Client.Auth.Services;
using TravelAgency.Client.Pages.Account.Detail;
using TravelAgency.Client.Pages.Countries.Detail;
using TravelAgency.Client.Pages.Locations;
using TravelAgency.Client.Pages.Locations.Detail;
using TravelAgency.Client.Pages.Reservations.Create;
using TravelAgency.Client.Pages.Reservations.Detail;
using TravelAgency.Client.Pages.Residences;
using TravelAgency.Client.Pages.Residences.Detail;
using TravelAgency.Client.Services;

namespace TravelAgency.Client.Pages
{
    public partial class AppShell : Shell
    {
        private AuthService _authService;
        private SettingsService _settingsService;
        private HttpService _httpService;

        public AppShell()
        {
            InitializeComponent();
            _authService = ServiceProviderHelper.GetService<AuthService>()!;
            _httpService = ServiceProviderHelper.GetService<HttpService>()!;
            _settingsService = ServiceProviderHelper.GetService<SettingsService>()!;
            BindingContext = _settingsService;

            Routing.RegisterRoute(nameof(CountryDetailPage), typeof(CountryDetailPage));
            Routing.RegisterRoute(nameof(LocationsPage), typeof(LocationsPage));
            Routing.RegisterRoute(nameof(ResidencesPage), typeof(ResidencesPage));
            Routing.RegisterRoute(nameof(LocationDetailPage), typeof(LocationDetailPage));
            Routing.RegisterRoute(nameof(ResidenceDetailPage), typeof(ResidenceDetailPage));
            Routing.RegisterRoute(nameof(ReservationDetailPage), typeof(ReservationDetailPage));
            Routing.RegisterRoute(nameof(FlightDetailPage), typeof(FlightDetailPage));
            Routing.RegisterRoute(nameof(ProfileDetailPage), typeof(ProfileDetailPage));
            Routing.RegisterRoute(nameof(CreateReservationPage), typeof(CreateReservationPage));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await _authService.LoadTokenFromStorage();
            if (_authService.HasAuthToken)
            {
                await _httpService.TestLogin();
            }
        }
    }
}