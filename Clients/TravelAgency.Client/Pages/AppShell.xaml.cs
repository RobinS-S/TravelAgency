using TravelAgency.Client.Auth.Services;

namespace TravelAgency.Client.Pages
{
    public partial class AppShell : Shell
    {
        private readonly AuthService _authService;

        public AppShell()
        {
            InitializeComponent();
            _authService = ServiceProviderHelper.GetService<AuthService>();
        }
    }
}