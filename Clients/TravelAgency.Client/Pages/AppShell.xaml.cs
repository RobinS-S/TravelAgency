using TravelAgency.Client.Auth.Services;
using TravelAgency.Client.Pages.Countries.Detail;

namespace TravelAgency.Client.Pages
{
    public partial class AppShell : Shell
    {
        private readonly AuthService _authService;

        public AppShell()
        {
            InitializeComponent();
            _authService = ServiceProviderHelper.GetService<AuthService>()!;

            Routing.RegisterRoute(nameof(CountryDetailPage), typeof(CountryDetailPage));
        }
    }
}