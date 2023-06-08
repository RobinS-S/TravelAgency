using TravelAgency.Shared.Airports;

namespace TravelAgency.Client.Pages.Reservations.Detail
{
    public partial class FlightDetailPage : ContentPage
    {
        private readonly FlightDetailPageViewModel _viewModel;

        public FlightDetailPage(FlightDetailPageViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += ViewModel_PropertyChanged;
            BindingContext = _viewModel;

            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "Reservation" || _viewModel.Reservation == null) return;

            map.Pins.Clear();

            map.AddPin(_viewModel.Reservation.Residence.Coordinates.Latitude, _viewModel.Reservation.Residence.Coordinates.Longitude, Colors.Red, 0.7F, "Residence", "", true, 19);

            if (_viewModel.Reservation.Flights != null && _viewModel.Reservation.Flights.Any())
            {
                var flight = _viewModel.Reservation.Flights.First();
                var fromAirport = AirTravelInformation.GetAirportByIATACode(flight.AirportCode);
                var destAirport = AirTravelInformation.GetAirportByIATACode(flight.DestinationAirportCode);
                map.AddPin(fromAirport!.GeoCoordinates.Latitude, fromAirport.GeoCoordinates.Longitude, Colors.Blue, 0.7F, fromAirport.Name, "", false);
                map.AddPin(destAirport!.GeoCoordinates.Latitude, destAirport.GeoCoordinates.Longitude, Colors.Blue, 0.7F, destAirport.Name, "", false);
            }
        }
    }
}