namespace TravelAgency.Domain.Entities
{
    public class Image : Entity
    {
        public string ImageUrl { get; set; } = null!;

        public ApplicationUser? User { get; set; } = null;

        public string? UserId { get; set; }

        public Image(string imageUrl, ApplicationUser? user = null)
        {
            ImageUrl = imageUrl;
            User = user;
        }

        public Image()
        {
        }
    }
}
