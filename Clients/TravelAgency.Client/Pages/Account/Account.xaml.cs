using TravelAgency.Client.Auth.Pages;
using TravelAgency.Client.Auth.Services;

namespace TravelAgency.Client.Pages.Account;

public partial class AccountPage : AuthenticatedContentPage
{
    public AccountPage(AuthService authService) : base(authService)
    {
        InitializeComponent();
    }
}