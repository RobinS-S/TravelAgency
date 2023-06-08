using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TravelAgency.Client.Pages.Reservations.Detail;
using TravelAgency.Client.Repositories;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.Pages.Reservations
{
    public partial class ReservationsPageViewModel : ObservableObject
    {
        private readonly ReservationRepository _reservationRepository;

        [ObservableProperty]
        private bool _isRefreshing;

        [ObservableProperty]
        private bool _errorStateEnabled;

        [ObservableProperty]
        private List<ReservationDto> _reservationsList = new();

        public ReservationsPageViewModel(ReservationRepository reservationRepository)
        {
            this._reservationRepository = reservationRepository;
        }

        [RelayCommand]
        private async Task LoadData()
        {
            IsRefreshing = true;
            var reservations = await _reservationRepository.GetMineAsync();
            if (reservations != null)
            {
                ReservationsList = reservations.OrderByDescending(r => r.Start).ToList();
            }
            ErrorStateEnabled = reservations == null;
            IsRefreshing = false;
        }

        [RelayCommand]
        public async Task ViewDetails(long id) => await Shell.Current.GoToAsync(nameof(ReservationDetailPage), new Dictionary<string, object> { { "id", id } });
    }
}
