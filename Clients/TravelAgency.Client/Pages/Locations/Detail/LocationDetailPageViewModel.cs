using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mapsui.UI.Maui;
using TravelAgency.Client.Pages.Residences;
using TravelAgency.Client.Repositories;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.Pages.Locations.Detail
{
    [QueryProperty("Id", "id")]
    public partial class LocationDetailPageViewModel : ObservableObject
    {
        private readonly LocationRepository locationRepository;

        [ObservableProperty]
        private LocationDto? _location;

        [ObservableProperty]
        private long _id;

        [ObservableProperty]
        private bool _isRefreshing;

        [ObservableProperty]
        private bool _errorStateEnabled;

        [ObservableProperty]
        private List<Pin> _pins = new();

        public LocationDetailPageViewModel(LocationRepository locationRepository)
        {
            this.locationRepository = locationRepository;
        }

        [RelayCommand]
        private async Task LoadData()
        {
            IsRefreshing = true;
            var location = await locationRepository.GetByIdAsync(Id);
            if (location != null)
            {
                Location = location;
            }
            ErrorStateEnabled = location == null;
            IsRefreshing = false;
        }

        [RelayCommand]
        private async Task ViewResidences()
        {
            if (Location != null)
            {
                await Shell.Current.GoToAsync(nameof(ResidencesPage), new Dictionary<string, object> { { "location", Location } });
            }
        }

        async partial void OnIdChanged(long value)
        {
            await LoadData();
        }
    }
}