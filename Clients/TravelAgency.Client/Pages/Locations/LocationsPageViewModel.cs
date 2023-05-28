﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TravelAgency.Client.Pages.Countries.Detail;
using TravelAgency.Client.Pages.Residences;
using TravelAgency.Client.Repositories;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.Pages.Locations
{
    [QueryProperty("Country", "country")]
    public partial class LocationsPageViewModel : ObservableObject
    {
        private readonly LocationRepository locationRepository;

        [ObservableProperty]
        private bool _isRefreshing;

        [ObservableProperty]
        private bool _errorStateEnabled;

        [ObservableProperty]
        private CountryDto? _country;

        [ObservableProperty]
        private ObservableCollection<LocationDto> _locationsList = new();

        public LocationsPageViewModel(LocationRepository locationRepository)
        {
            this.locationRepository = locationRepository;
        }

        [RelayCommand]
        private async Task LoadData()
        {
            IsRefreshing = true;
            var locations = Country == null ? await locationRepository.GetAllAsync() : await locationRepository.GetAllByCountryIdAsync(Country.Id);
            if (locations != null)
            {
                LocationsList = new(locations);
            }
            ErrorStateEnabled = locations == null;
            IsRefreshing = false;
        }

        [RelayCommand]
        public async Task ViewDetails(LocationDto location) => await Shell.Current.GoToAsync(nameof(ResidencesPage), new Dictionary<string, object> { { "location", location } });
    }
}
