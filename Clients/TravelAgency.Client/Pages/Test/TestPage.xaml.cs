using TravelAgency.Client.Auth.Pages;
using TravelAgency.Client.Auth.Services;

namespace TravelAgency.Client.Pages.Test;

public partial class TestPage : AuthenticatedContentPage
{
    public TestPage(AuthService authService) : base(authService)
    {
        InitializeComponent();
    }
}