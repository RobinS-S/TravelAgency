using TravelAgency.Domain.Entities;

namespace TravelAgency.Domain.Interfaces
{
    public interface IImageOwningEntity
    {
        public ICollection<EntityImage> Images { get; set; }
    }
}
