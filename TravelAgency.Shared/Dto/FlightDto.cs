namespace TravelAgency.Shared.Dto
{
    public class FlightDto
    {
        public DateTime FromDeparture { get; set; }

        public DateTime Until { get; set; }

        public string AirportCode { get; set; } = null!;

        public string DestinationAirportCode { get; set; } = null!;

        public string FlightNumber { get; set; } = null!;

        public ICollection<string> Seats { get; set; } = null!;

        public GeoCoordinatesDto Coordinates { get; set; } = null!;

        public string OwnerId { get; set; } = null!;
    }
}