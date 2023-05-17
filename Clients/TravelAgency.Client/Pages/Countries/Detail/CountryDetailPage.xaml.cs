using Mapsui.UI.Maui;
using TravelAgency.Client.Services;

namespace TravelAgency.Client.Pages.Countries.Detail
{
    public partial class CountryDetailPage : ContentPage
    {
        private readonly CountryDetailPageViewModel _viewModel;
        private readonly GeolocationService _geolocationService;
        private Pin? _countryPin;

        public CountryDetailPage()
        {
            _geolocationService = ServiceProviderHelper.GetService<GeolocationService>()!;
            _viewModel = ServiceProviderHelper.GetService<CountryDetailPageViewModel>()!;
            _viewModel.PropertyChanged += ViewModel_PropertyChanged;
            BindingContext = _viewModel;

            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await Init();
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

        private async Task Init()
        {
            await _geolocationService.GetLocation();
            //map.AddPin(_geolocationService.Location.Latitude, _geolocationService.Location.Longitude, Colors.Blue, 0.7F, "You", "You", false);
        }
    }
}