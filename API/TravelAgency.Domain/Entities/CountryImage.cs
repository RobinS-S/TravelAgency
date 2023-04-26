namespace TravelAgency.Domain.Entities
{
    public class CountryImage : Entity
    {
        public bool IsPrimary { get; set; }
        public long ImageId { get; set; }

        public CountryImage()
        {
        }

        public CountryImage(bool isPrimary, long imageId)
        {
            IsPrimary = isPrimary;
            ImageId = imageId;
        }
    }
}
