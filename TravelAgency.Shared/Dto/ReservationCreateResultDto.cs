using TravelAgency.Shared.Enum;

namespace TravelAgency.Shared.Dto
{
    public class ReservationCreateResultDto
    {
        public ReservationCreateResultType ResultType { get; set; }

        public ReservationDto? Reservation { get; set; }

        public ReservationCreateResultDto(ReservationCreateResultType result, ReservationDto? reservation = null)
        {
            ResultType = result;
            Reservation = reservation;
        }

        public ReservationCreateResultDto()
        {
        }
    }
}
