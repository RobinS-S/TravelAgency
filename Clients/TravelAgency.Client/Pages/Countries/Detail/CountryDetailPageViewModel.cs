using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mapsui.UI.Maui;
using TravelAgency.Client.Pages.Locations;
using TravelAgency.Client.Repositories;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.Pages.Countries.Detail
{
    [QueryProperty("Id", "id")]
    public partial class CountryDetailPageViewModel : ObservableObject
    {
        private readonly CountryRepository countryRepository;

        [ObservableProperty]
        private CountryDto? _country;

        [ObservableProperty]
        private long _id;

        [ObservableProperty]
        private bool _isRefreshing;

        [ObservableProperty]
        private bool _errorStateEnabled;

        [ObservableProperty]
        private List<Pin> _pins = new();

        public CountryDetailPageViewModel(CountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        [RelayCommand]
        private async Task LoadData()
        {
            IsRefreshing = true;
            var country = await countryRepository.GetByIdAsync(Id);
            if (country != null)
            {
                Country = country;
            }
            ErrorStateEnabled = country == null;
            IsRefreshing = false;
        }

        [RelayCommand]
        private async Task ViewLocations()
        {
            if(Country != null)
            {
                await Shell.Current.GoToAsync(nameof(LocationsPage), new Dictionary<string, object> { { "country", Country } });
            }
        }

        async partial void OnIdChanged(long value)
        {
            await LoadData();
        }

        partial void OnCountryChanged(CountryDto? value)
        {
            if(Country != null)
            {
                AddPin(Country.Coordinates.Latitude, Country.Coordinates.Longitude, Colors.Red);
            }
        }

        public Pin AddPin(double latitude, double longitude, Color c, string label = "Pin", string address = "")
        {
            var myPin = new Pin()
            {
                Position = new Position(latitude, longitude),
                Type = PinType.Pin,
                Label = label,
                Address = address,
                Scale = 0.7F,
                Color = c,
            };
            Pins.Add(myPin);
            return myPin;
        }
    }
}