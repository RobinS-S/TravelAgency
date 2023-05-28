using Mapsui.UI.Maui;

namespace TravelAgency.Client.Pages.Locations.Detail
{
    public partial class LocationDetailPage : ContentPage
    {
        private readonly LocationDetailPageViewModel _viewModel;
        private Pin? _locationPin;

        public LocationDetailPage(LocationDetailPageViewModel viewModel)
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
            if (e.PropertyName == "Location" && _viewModel.Location != null)
            {
                if (_locationPin != null)
                {
                    map.Pins.Remove(_locationPin);
                    _locationPin.Dispose();
                }

                _locationPin = map.AddPin(_viewModel.Location.Coordinates.Latitude, _viewModel.Location.Coordinates.Longitude, Colors.Red, 0.7F, "Location", "", true, 11);
            }
        }
    }
}