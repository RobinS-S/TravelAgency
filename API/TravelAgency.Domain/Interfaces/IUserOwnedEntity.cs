using TravelAgency.Domain.Entities;

namespace TravelAgency.Domain.Interfaces
{
    public interface IUserOwnedEntity
    {
        public ApplicationUser Owner { get; set; }

        public string OwnerId { get; set; }
    }
}
