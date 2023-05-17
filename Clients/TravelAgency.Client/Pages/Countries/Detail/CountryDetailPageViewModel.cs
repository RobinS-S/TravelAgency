using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mapsui.UI.Maui;
using System.ComponentModel;
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
        private List<Pin> _pins = new();

        public IAsyncRelayCommand LoadDataCommand;

        public CountryDetailPageViewModel(CountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
            LoadDataCommand = new AsyncRelayCommand(LoadData);
        }

        private async Task LoadData()
        {
            IsRefreshing = true;
            var country = await countryRepository.GetByIdAsync(Id);
            if (country != null)
            {
                Country = country;
            }
            IsRefreshing = false;
        }

        protected override async void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if(e.PropertyName == nameof(Id))
            {
                await LoadData();
            }

            else if(e.PropertyName == nameof(Country) && Country != null)
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