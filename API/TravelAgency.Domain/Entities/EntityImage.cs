namespace TravelAgency.Domain.Entities
{
    public class EntityImage : Entity
    {
        public bool IsPrimary { get; set; }
        public long ImageId { get; set; }

        public EntityImage()
        {
        }

        public EntityImage(bool isPrimary, long imageId)
        {
            IsPrimary = isPrimary;
            ImageId = imageId;
        }
    }
}
