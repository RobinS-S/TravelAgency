using TravelAgency.Domain.Interfaces;

namespace TravelAgency.Domain.Entities
{
    public class Image : Entity, IUserOwnedEntity
    {
        public string ImageUrl { get; set; } = null!;

        public ApplicationUser Owner { get; set; } = null!;

        public string OwnerId { get; set; } = null!;

        public Image(string imageUrl, ApplicationUser user)
        {
            ImageUrl = imageUrl;
            Owner = user;
        }

        public Image()
        {
        }
    }
}
