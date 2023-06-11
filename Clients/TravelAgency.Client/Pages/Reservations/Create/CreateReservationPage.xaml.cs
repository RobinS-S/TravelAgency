using TravelAgency.Client.Auth.Pages;
using TravelAgency.Client.Auth.Services;

namespace TravelAgency.Client.Pages.Reservations.Create;

public partial class CreateReservationPage : AuthenticatedContentPage
{
    private readonly CreateReservationPageViewModel _viewModel;

    public CreateReservationPage(CreateReservationPageViewModel viewModel, AuthService authService) : base(authService)
    {
        _viewModel = viewModel;
        BindingContext = _viewModel;

        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadDataCommand.ExecuteAsync(null);
    }
}