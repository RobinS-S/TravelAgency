using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mapsui.UI.Maui;
using TravelAgency.Shared.Airports;
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

        [RelayCommand]
        private async Task OpenAirportLocationForFlight(FlightDto flight)
        {
            var airport = AirTravelInformation.GetAirportByIATACode(flight.AirportCode);
            var googleMapsUrl =
                $"https://www.google.com/maps/search/?api=1&query={airport!.Country}%20{airport!.IATACode}%20airport";

            try
            {
                if (await Launcher.CanOpenAsync(googleMapsUrl))
                {
                    await Launcher.OpenAsync(googleMapsUrl);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}