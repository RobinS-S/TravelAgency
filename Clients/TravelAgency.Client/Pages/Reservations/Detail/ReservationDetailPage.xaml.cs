using Mapsui.UI.Maui;

namespace TravelAgency.Client.Pages.Reservations.Detail
{
    public partial class ReservationDetailPage : ContentPage
    {
        private readonly ReservationDetailPageViewModel _viewModel;
        private Pin? _residencePin;

        public ReservationDetailPage(ReservationDetailPageViewModel viewModel)
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

            if (_residencePin != null)
            {
                map.Pins.Remove(_residencePin);
                _residencePin.Dispose();
            }

            _residencePin = map.AddPin(_viewModel.Reservation.Residence.Coordinates.Latitude, _viewModel.Reservation.Residence.Coordinates.Longitude, Colors.Red, 0.7F, "Residence", "", true, 19);
        }
    }
}