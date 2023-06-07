namespace TravelAgency.Domain.Entities
{
    public class FlightSeat
    {
        public string SeatNumber { get; set; } = null!;

        public FlightSeat()
        {
        }

        public FlightSeat(string seatNumber)
        {
            SeatNumber = seatNumber;
        }
    }
}
