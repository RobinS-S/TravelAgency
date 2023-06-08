using Microsoft.AspNetCore.Identity;

namespace TravelAgency.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public bool HasWhatsApp { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ApplicationUser user &&
                   Id == user.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
