using Mapsui.UI.Maui;

namespace TravelAgency.Client.Pages.Countries.Detail
{
    public partial class CountryDetailPage : ContentPage
    {
        private readonly CountryDetailPageViewModel _viewModel;
        private Pin? _countryPin;

        public CountryDetailPage(CountryDetailPageViewModel viewModel)
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
            if (e.PropertyName == "Country" && _viewModel.Country != null)
            {
                if (_countryPin != null)
                {
                    map.Pins.Remove(_countryPin);
                    _countryPin.Dispose();
                }

                _countryPin = map.AddPin(_viewModel.Country.Coordinates.Latitude, _viewModel.Country.Coordinates.Longitude, Colors.Red, 0.7F, "Country");
            }
        }
    }
}