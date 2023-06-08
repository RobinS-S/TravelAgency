using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mapsui.UI.Maui;
using TravelAgency.Client.Pages.Account.Detail;
using TravelAgency.Client.Pages.Residences.Detail;
using TravelAgency.Client.Repositories;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.Pages.Reservations.Detail
{
    [QueryProperty("Id", "id")]
    public partial class ReservationDetailPageViewModel : ObservableObject
    {
        private readonly ReservationRepository _reservationRepository;

        [ObservableProperty]
        private ReservationDto? _reservation;

        [ObservableProperty]
        private long _id;

        [ObservableProperty]
        private bool _isRefreshing;

        [ObservableProperty]
        private bool _errorStateEnabled;

        [ObservableProperty]
        private List<Pin> _pins = new();

        public ReservationDetailPageViewModel(ReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        [RelayCommand]
        private async Task LoadData()
        {
            IsRefreshing = true;
            var reservation = await _reservationRepository.GetByIdAsync(Id);
            if (reservation != null)
            {
                Reservation = reservation;
            }
            ErrorStateEnabled = reservation == null;
            IsRefreshing = false;
        }

        async partial void OnIdChanged(long value)
        {
            await LoadData();
        }

        [RelayCommand]
        public async Task ViewResidenceDetails() => await Shell.Current.GoToAsync(nameof(ResidenceDetailPage), new Dictionary<string, object> { { "id", Reservation!.Residence!.Id! } });

        [RelayCommand]
        public async Task ViewFlightsDetails() => await Shell.Current.GoToAsync(nameof(FlightDetailPage), new Dictionary<string, object> { { "reservation", Reservation! } });

        [RelayCommand]
        public async Task ViewOwnerDetails() => await Shell.Current.GoToAsync(nameof(ProfileDetailPage), new Dictionary<string, object> { { "id", Reservation!.OwnerId } });
    }
}