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
        private readonly CountryRepository _countryRepository;

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
            this._countryRepository = countryRepository;
        }

        [RelayCommand]
        private async Task LoadData()
        {
            IsRefreshing = true;
            var country = await _countryRepository.GetByIdAsync(Id);
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
            if (Country != null)
            {
                await Shell.Current.GoToAsync(nameof(LocationsPage), new Dictionary<string, object> { { "country", Country } });
            }
        }

        async partial void OnIdChanged(long value)
        {
            await LoadData();
        }
    }
}