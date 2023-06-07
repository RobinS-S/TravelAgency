namespace TravelAgency.Shared.Dto
{
    public class ReservationDto
    {
        public ResidenceDto Residence { get; set; } = null!;

        public long ResidenceId { get; set; }

        public string TenantId { get; set; } = null!;

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public decimal Cost { get; set; }

        public string OwnerId { get; set; } = null!;

        public ICollection<FlightDto> Flights { get; set; } = null!;
    }
}
