using TravelAgency.Domain.Interfaces;

namespace TravelAgency.Domain.Entities
{
    public class Reservation : Entity, IUserOwnedEntity
    {
        public Residence Residence { get; set; } = null!;

        public long ResidenceId { get; set; }

        public ApplicationUser User { get; set; } = null!;

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public decimal Cost { get; set; }

        public ApplicationUser Owner { get; set; } = null!;

        public Reservation()
        {
        }

        public Reservation(Residence residence, ApplicationUser user, DateTime start, DateTime end, decimal cost)
        {
            Residence = residence;
            User = user;
            Start = start;
            End = end;
            Cost = cost;
        }
    }
}
