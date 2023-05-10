namespace TravelAgency.Shared.Dto
{
    public class ImageDto
    {
        public long? Id { get; set; }
        public string ImageUrl { get; set; } = null!;

        public ImageDto(string imageUrl)
        {
            ImageUrl = imageUrl;
        }
    }
}
