using TravelAgency.Client.Auth.Pages;
using TravelAgency.Client.Auth.Services;

namespace TravelAgency.Client.Pages.Reservations;

public partial class ReservationsPage : AuthenticatedContentPage
{
    private readonly AuthService _authService;
    private readonly ReservationsPageViewModel _viewModel;

    public ReservationsPage(AuthService authService, ReservationsPageViewModel viewModel) : base(authService)
    {
        _authService = authService;
        _viewModel = viewModel;
        BindingContext = _viewModel;

        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_authService.HasAuthToken)
        {
            await _viewModel.LoadDataCommand.ExecuteAsync(null);
        }
    }
}