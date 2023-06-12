using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TravelAgency.Client.Pages.Locations.Detail;
using TravelAgency.Client.Repositories;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.Pages.Locations
{
    [QueryProperty("Country", "country")]
    public partial class LocationsPageViewModel : ObservableObject
    {
        private readonly LocationRepository _locationRepository;

        [ObservableProperty]
        private bool _isRefreshing;

        [ObservableProperty]
        private bool _errorStateEnabled;

        [ObservableProperty]
        private CountryDto? _country;

        [ObservableProperty]
        private List<LocationDto> _locationsList = new();

        public LocationsPageViewModel(LocationRepository locationRepository)
        {
            this._locationRepository = locationRepository;
        }

        [RelayCommand]
        private async Task LoadData()
        {
            IsRefreshing = true;
            var locations = Country == null ? await _locationRepository.GetAllAsync() : await _locationRepository.GetAllByCountryIdAsync(Country.Id);
            if (locations != null)
            {
                LocationsList = locations;
                if (OperatingSystem.IsAndroid())
                {
                    await Task.Delay(250);
                    LocationsList = locations;
                }
            }
            ErrorStateEnabled = locations == null;
            IsRefreshing = false;
        }

        [RelayCommand]
        public async Task ViewDetails(long id) => await Shell.Current.GoToAsync(nameof(LocationDetailPage), new Dictionary<string, object> { { "id", id } });
    }
}
