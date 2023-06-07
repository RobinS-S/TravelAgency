namespace TravelAgency.Domain.Entities
{
    public class Flight
    {
        public DateTime FromDeparture { get; set; }

        public DateTime Until { get; set; }

        public string AirportCode { get; set; } = null!;

        public string DestinationAirportCode { get; set; } = null!;

        public string FlightNumber { get; set; } = null!;

        public ICollection<FlightSeat> Seats { get; set; } = null!;

        public GeoCoordinates Coordinates { get; set; } = null!;

        public ApplicationUser Owner { get; set; } = null!;

        public string OwnerId { get; set; } = null!;

        public Flight()
        {
        }

        public Flight(DateTime fromDeparture, DateTime until, string airportCode, string destinationAirportCode, string flightNumber, ICollection<FlightSeat> seats, GeoCoordinates coordinates, ApplicationUser owner)
        {
            FromDeparture = fromDeparture;
            Until = until;
            AirportCode = airportCode;
            DestinationAirportCode = destinationAirportCode;
            FlightNumber = flightNumber;
            Seats = seats;
            Coordinates = coordinates;
            Owner = owner;
        }
    }
}
