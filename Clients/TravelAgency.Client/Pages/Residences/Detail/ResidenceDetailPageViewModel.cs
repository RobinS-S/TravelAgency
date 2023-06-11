using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mapsui.UI.Maui;
using TravelAgency.Client.Pages.Reservations.Create;
using TravelAgency.Client.Repositories;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.Pages.Residences.Detail
{
    [QueryProperty("Id", "id")]
    public partial class ResidenceDetailPageViewModel : ObservableObject
    {
        private readonly ResidenceRepository _residenceRepository;

        [ObservableProperty]
        private ResidenceDto? _residence;

        [ObservableProperty]
        private long _id;

        [ObservableProperty]
        private bool _isRefreshing;

        [ObservableProperty]
        private bool _errorStateEnabled;

        [ObservableProperty]
        private List<Pin> _pins = new();

        public ResidenceDetailPageViewModel(ResidenceRepository residenceRepository)
        {
            this._residenceRepository = residenceRepository;
        }

        [RelayCommand]
        private async Task LoadData()
        {
            IsRefreshing = true;
            var residence = await _residenceRepository.GetByIdAsync(Id);
            if (residence != null)
            {
                Residence = residence;
            }
            ErrorStateEnabled = residence == null;
            IsRefreshing = false;
        }

        async partial void OnIdChanged(long value)
        {
            await LoadData();
        }

        [RelayCommand]
        public async Task ViewDetails() => await Shell.Current.GoToAsync(nameof(CreateReservationPage), new Dictionary<string, object> { { "residenceId", Residence!.Id! } });
    }
}