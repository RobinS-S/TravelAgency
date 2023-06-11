using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Repositories.Interfaces;
using TravelAgency.Shared.Airports;
using TravelAgency.Shared.Dto;
using TravelAgency.Shared.Enum;
using TravelAgency.Shared.Travel;

namespace TravelAgency.Application.Services
{
    public class ReservationService
    {
        private readonly IReservationRepository reservationRepository;
        private readonly IResidenceRepository residenceRepository;
        private readonly ILocationRepository _locationRepository;

        public ReservationService(IReservationRepository reservationRepository, IResidenceRepository residenceRepository, ILocationRepository locationRepository)
        {
            this.reservationRepository = reservationRepository;
            this.residenceRepository = residenceRepository;
            _locationRepository = locationRepository;
        }

        public async Task<ReservationCreateResult> CreateReservation(CreateReservationDto reservationDto, ApplicationUser tenant)
        {
            var residence = await residenceRepository.GetByIdAsync(reservationDto.ResidenceId);
            if (residence == null)
            {
                return new ReservationCreateResult(ReservationCreateResultType.UnknownError);
            }

            var location = await _locationRepository.GetByIdAsync(residence.LocationId);

            var activeReservation = await reservationRepository.GetActiveByTenantIdAsync(tenant.Id);
            if (activeReservation != null)
            {
                return new ReservationCreateResult(ReservationCreateResultType.AlreadyHaveActiveReservation);
            }

            var reservationsInTimeFrame = await reservationRepository.GetAllByResidenceIdAndBetweenAsync(reservationDto.ResidenceId, reservationDto.Start, reservationDto.End);
            if (reservationsInTimeFrame.Any())
            {
                return new ReservationCreateResult(ReservationCreateResultType.TimespanNotAvailable);
            }

            var isShorterThanOneDay = CalculateDaysBetween(reservationDto.Start, reservationDto.End) <= 0;
            if (isShorterThanOneDay)
            {
                return new ReservationCreateResult(ReservationCreateResultType.TooShort);
            }

            var isFlightIncludedMismatch = reservationDto.FlightIncluded && string.IsNullOrWhiteSpace(reservationDto.FromAirportIATACode);
            if (isFlightIncludedMismatch)
            {
                return new ReservationCreateResult(ReservationCreateResultType.UnknownError);
            }

            var price = residence.PricePerDay * CalculateDaysBetween(reservationDto.Start, reservationDto.End);
            List<Flight> flights = new();
            if (reservationDto.FlightIncluded && reservationDto.FromAirportIATACode != null)
            {
                var airport = AirTravelInformation.GetAirportByIATACode(reservationDto.FromAirportIATACode);
                if (airport == null)
                {
                    return new ReservationCreateResult(ReservationCreateResultType.UnknownError);
                }

                var destinationAirport = AirTravelInformation.GetNearestAirport(new(residence.Coordinates.Latitude, residence.Coordinates.Longitude));

                var flightTravelTime = AirTravelInformation.CalculateFlightTravelTimeMinutes(airport, destinationAirport);
                var randomOffset = Random.Shared.Next(10, 30);

                var departureToTime = reservationDto.Start - TimeSpan.FromMinutes(flightTravelTime + 120 + randomOffset);
                var arrivalToTime = departureToTime + TimeSpan.FromMinutes(flightTravelTime + Random.Shared.Next(5, 20));

                var departureWayBack = reservationDto.End + TimeSpan.FromMinutes(Random.Shared.Next(30, 120));
                var departureArrivalTime = departureWayBack + TimeSpan.FromMinutes(flightTravelTime + Random.Shared.Next(5, 20));

                // From - To flight
                flights.Add(
                    new Flight(departureToTime, arrivalToTime, airport.IATACode, destinationAirport.IATACode,
                        Airlines.GenerateRandomFlightNumber(),
                        SeatNumbers.AssignRandomSeatNumbers(reservationDto.AmountOfPeople)
                            .Select(s => new FlightSeat(s))
                            .ToList(),
                        new Domain.GeoCoordinates(airport.GeoCoordinates.Latitude, airport.GeoCoordinates.Longitude),
                        tenant));

                // To - From flight (way back)
                flights.Add(
                    new Flight(departureWayBack, departureArrivalTime, destinationAirport.IATACode, airport.IATACode,
                        Airlines.GenerateRandomFlightNumber(),
                        SeatNumbers.AssignRandomSeatNumbers(reservationDto.AmountOfPeople)
                            .Select(s => new FlightSeat(s))
                            .ToList(),
                        new Domain.GeoCoordinates(airport.GeoCoordinates.Latitude, airport.GeoCoordinates.Longitude),
                        tenant));

                price += reservationDto.AmountOfPeople * AirTravelInformation.EstimateFlightPrice(flightTravelTime);
            }

            var reservation = new Reservation(residence, tenant, location!.Owner, reservationDto.Start, reservationDto.End, price, flights, reservationDto.AmountOfPeople);
            await reservationRepository.AddAsync(reservation);
            return new ReservationCreateResult(ReservationCreateResultType.Succeeded, reservation);
        }

        public static int CalculateDaysBetween(DateTime date1, DateTime date2)
        {
            var span = date2.Subtract(date1);
            return Math.Abs((int)span.TotalDays);
        }
    }
}
