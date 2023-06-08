using CommunityToolkit.Mvvm.ComponentModel;
using Mapsui.UI.Maui;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.Pages.Reservations.Detail
{
    [QueryProperty("Reservation", "reservation")]
    public partial class FlightDetailPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ReservationDto? _reservation;

        [ObservableProperty]
        private List<Pin> _pins = new();

        public FlightDetailPageViewModel()
        {
        }
    }
}