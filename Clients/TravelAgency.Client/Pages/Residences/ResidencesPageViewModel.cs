using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TravelAgency.Client.Pages.Residences.Detail;
using TravelAgency.Client.Repositories;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.Pages.Residences
{
    [QueryProperty("Location", "location")]
    public partial class ResidencesPageViewModel : ObservableObject
    {
        private readonly ResidenceRepository residenceRepository;

        [ObservableProperty]
        private bool _isRefreshing;

        [ObservableProperty]
        private bool _errorStateEnabled;

        [ObservableProperty]
        private LocationDto? _location;

        [ObservableProperty]
        private ObservableCollection<ResidenceDto> _residencesList = new();

        public ResidencesPageViewModel(ResidenceRepository residenceRepository)
        {
            this.residenceRepository = residenceRepository;
        }

        [RelayCommand]
        private async Task LoadData()
        {
            IsRefreshing = true;
            var residences = Location == null ? await residenceRepository.GetAllAsync() : await residenceRepository.GetAllByLocationIdAsync(Location.Id!.Value);
            if (residences != null)
            {
                ResidencesList = new(residences);
            }
            ErrorStateEnabled = residences == null;
            IsRefreshing = false;
        }

        [RelayCommand]
        public async Task ViewDetails(long id) => await Shell.Current.GoToAsync(nameof(ResidenceDetailPage), new Dictionary<string, object> { { "id", id } });
    }
}
