using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TravelAgency.Client.Pages.Countries.Detail;
using TravelAgency.Client.Repositories;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.Pages.Countries
{
    public partial class CountriesPageViewModel : ObservableObject
    {
        private readonly CountryRepository countryRepository;

        [ObservableProperty]
        private bool _isRefreshing;

        [ObservableProperty]
        private bool _errorStateEnabled;

        [ObservableProperty]
        private ObservableCollection<CountryDto> _countriesList = new();

        public CountriesPageViewModel(CountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        [RelayCommand]
        private async Task LoadData()
        {
            IsRefreshing = true;
            var countries = await countryRepository.GetAllAsync();
            if (countries != null)
            {
                CountriesList = new(countries);
            }
            ErrorStateEnabled = countries == null;
            IsRefreshing = false;
        }

        [RelayCommand]
        public async Task ViewDetails(long id) => await Shell.Current.GoToAsync(nameof(CountryDetailPage), new Dictionary<string, object> { { "id", id } });
    }
}
