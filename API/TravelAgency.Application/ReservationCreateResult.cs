using TravelAgency.Domain.Entities;
using TravelAgency.Shared.Enum;

namespace TravelAgency.Application
{
    public class ReservationCreateResult
    {
        public ReservationCreateResultType ResultType { get; set; }

        public Reservation? Reservation { get; set; }

        public ReservationCreateResult(ReservationCreateResultType result, Reservation? reservation = null)
        {
            ResultType = result;
            Reservation = reservation;
        }
    }
}
