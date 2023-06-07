using TravelAgency.Domain.Interfaces;

namespace TravelAgency.Domain.Entities
{
    public class Reservation : Entity, IUserOwnedEntity
    {
        public Residence Residence { get; set; } = null!;

        public long ResidenceId { get; set; }

        public ApplicationUser Tenant { get; set; } = null!;

        public string TenantId { get; set; } = null!;

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public decimal Cost { get; set; }

        public ApplicationUser Owner { get; set; } = null!;

        public string OwnerId { get; set; } = null!;

        public ICollection<Flight> Flights { get; set; } = null!;

        public Reservation()
        {
        }

        public Reservation(Residence residence, ApplicationUser tenant, ApplicationUser owner, DateTime start, DateTime end, decimal cost, ICollection<Flight> flights)
        {
            Residence = residence;
            Tenant = tenant;
            Owner = owner;
            Start = start;
            End = end;
            Cost = cost;
            Flights = flights;
        }
    }
}
