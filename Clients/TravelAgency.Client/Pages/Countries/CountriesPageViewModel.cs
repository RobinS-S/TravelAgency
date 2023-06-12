using CommunityToolkit.Maui.Core.Extensions;
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
        private readonly CountryRepository _countryRepository;

        [ObservableProperty]
        private bool _isRefreshing;

        [ObservableProperty]
        private bool _errorStateEnabled;

        [ObservableProperty]
        private ObservableCollection<CountryDto> _countriesList = new();

        public CountriesPageViewModel(CountryRepository countryRepository)
        {
            this._countryRepository = countryRepository;
        }

        [RelayCommand]
        private async Task LoadData()
        {
            IsRefreshing = true;
            var countries = await _countryRepository.GetAllAsync();
            ErrorStateEnabled = countries == null;
            IsRefreshing = false;
            if (countries != null)
            {
                CountriesList = countries.ToObservableCollection();
                if (OperatingSystem.IsAndroid())
                {
                    await Task.Delay(250);
                    CountriesList = countries.ToObservableCollection();
                }
            }
        }

        [RelayCommand]
        public async Task ViewDetails(long id) => await Shell.Current.GoToAsync(nameof(CountryDetailPage), new Dictionary<string, object> { { "id", id } });
    }
}
