using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TravelAgency.Client.Pages.Reservations.Detail;
using TravelAgency.Client.Repositories;
using TravelAgency.Client.Resources.Localization;
using TravelAgency.Client.Services;
using TravelAgency.Shared.Airports;
using TravelAgency.Shared.Dto;
using TravelAgency.Shared.Enum;

namespace TravelAgency.Client.Pages.Reservations.Create
{
    [QueryProperty("ResidenceId", "residenceId")]
    public partial class CreateReservationPageViewModel : ObservableObject
    {
        private readonly ResidenceRepository _residenceRepository;
        private readonly ReservationRepository _reservationRepository;
        private readonly GeolocationService _geolocationService;

        [ObservableProperty]
        private List<ReservationPickedSpotDto> _spots = new();

        [ObservableProperty]
        private bool _isRefreshing;

        [ObservableProperty]
        private bool _errorStateEnabled;

        [ObservableProperty]
        private long _residenceId;

        [ObservableProperty]
        private ResidenceDto? _residence;

        [ObservableProperty]
        private string _amountOfPeople = "1";

        [ObservableProperty]
        private bool _isValid;

        [ObservableProperty]
        private bool _isTimeSlotTaken = true;

        [ObservableProperty]
        private DateTime _start = DateTime.Now;

        [ObservableProperty]
        private DateTime _end = DateTime.Now;

        [ObservableProperty]
        private decimal _price;

        [ObservableProperty]
        private bool _flightIncluded = true;

        [ObservableProperty]
        private string? _airportIataCode;

        [ObservableProperty]
        private string? _errorString;

        public CreateReservationPageViewModel(ReservationRepository reservationRepository, ResidenceRepository residenceRepository, GeolocationService geolocationService)
        {
            _reservationRepository = reservationRepository;
            _residenceRepository = residenceRepository;
            _geolocationService = geolocationService;
        }

        [RelayCommand]
        private async Task LoadData()
        {
            IsRefreshing = true;
            var spots = await _reservationRepository.GetAllBetweenAsync(ResidenceId, DateTime.Now - TimeSpan.FromHours(6), DateTime.Now.AddYears(5));
            if (spots != null)
            {
                Spots = spots.OrderByDescending(s => s.Start).ToList();
            }

            var residence = await _residenceRepository.GetByIdAsync(ResidenceId);
            if (residence != null)
            {
                Residence = residence;
            }
            await _geolocationService.GetLocation();
            ErrorStateEnabled = spots == null || residence == null;
            IsRefreshing = false;
            await UpdateCost();
        }

        [RelayCommand]
        private async Task CreateReservation()
        {
            if (!IsValid || IsTimeSlotTaken)
            {
                return;
            }

            var createReservationDto = new CreateReservationDto()
            {
                AmountOfPeople = int.Parse(AmountOfPeople),
                Start = Start,
                End = End,
                ResidenceId = ResidenceId,
                FromAirportIATACode = AirportIataCode,
                FlightIncluded = FlightIncluded
            };

            var result = await _reservationRepository.CreateAsync(createReservationDto);
            if (result != null)
            {
                if (result.ResultType == ReservationCreateResultType.Succeeded && result.Reservation != null)
                {
                    ErrorString = null;
                    await ViewDetails(result.Reservation.Id!.Value);
                }
                else
                {
                    var key = $"CreateReservationError.{result.ResultType}";
                    ErrorString = Translations.ResourceManager.GetString(key) ?? key;
                }
            }
            else
            {
                ErrorString = Translations.Error;
            }
        }

        [RelayCommand]
        private Task UpdateCost()
        {
            if (_geolocationService.Location != null && AirportIataCode == null)
            {
                var closestAirport = AirTravelInformation.GetNearestAirport(new GeoCoordinatesDto(_geolocationService.Location.Latitude, _geolocationService.Location.Longitude));
                AirportIataCode = closestAirport.IATACode;
            }
            decimal cost = 0;
            if (string.IsNullOrWhiteSpace(AmountOfPeople) || !int.TryParse(AmountOfPeople, out var amountOfPeople) || Residence == null || AirportIataCode == null)
                return Task.CompletedTask;

            var airport = AirTravelInformation.GetAirportByIATACode(AirportIataCode);
            if (airport == null)
            {
                return Task.CompletedTask;
            }

            cost += amountOfPeople * Residence.PricePerDay;
            if (FlightIncluded)
            {
                var destinationAirport = AirTravelInformation.GetNearestAirport(new(Residence.Coordinates.Latitude, Residence.Coordinates.Longitude));
                var flightTravelTime = AirTravelInformation.CalculateFlightTravelTimeMinutes(airport, destinationAirport);
                cost += amountOfPeople * AirTravelInformation.EstimateFlightPrice(flightTravelTime);
            }

            Price = cost;
            return Task.CompletedTask;
        }

        private void CheckValidation()
        {
            if (string.IsNullOrWhiteSpace(AmountOfPeople)
                || !int.TryParse(AmountOfPeople, out var amountOfPeople)
                || CheckSpotTaken()
                || amountOfPeople < 1
                || amountOfPeople > 20
                || (End - Start).Days <= 0)
            {
                if (IsValid)
                {
                    IsValid = false;
                }
            }
            else
            {
                if (!IsValid)
                {
                    IsValid = true;
                }
            }

            UpdateCost();
        }

        private bool CheckSpotTaken()
        {
            if (Start >= End || Spots.Any(spot => Start < spot.End && spot.Start < End))
            {
                if (!IsTimeSlotTaken)
                {
                    IsTimeSlotTaken = true;
                }
            }
            else
            {
                if (IsTimeSlotTaken)
                {
                    IsTimeSlotTaken = false;
                }
            }
            return IsTimeSlotTaken;
        }


        [RelayCommand]
        public async Task ViewDetails(long id) => await Shell.Current.GoToAsync(nameof(ReservationDetailPage), new Dictionary<string, object> { { "id", id } });

        partial void OnStartChanged(DateTime value) => CheckValidation();

        partial void OnEndChanged(DateTime value) => CheckValidation();

        partial void OnAmountOfPeopleChanged(string value) => CheckValidation();

        partial void OnSpotsChanged(List<ReservationPickedSpotDto> value) => CheckValidation();

        partial void OnFlightIncludedChanged(bool value)
        {
            CheckValidation();
        }
    }
}
