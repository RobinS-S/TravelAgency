namespace TravelAgency.Shared.Dto
{
    public class CreateReservationDto
    {
        public long ResidenceId { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string? FromAirportIATACode { get; set; }

        public int AmountOfPeople { get; set; }

        public bool FlightIncluded { get; set; }
    }
}
