using TravelAgency.Domain.Helpers;
using TravelAgency.Domain.Interfaces;

namespace TravelAgency.Domain.Entities
{
    public class Country : Entity, IImageOwningEntity, IGeolocationOwningEntity
    {
        public string IsoName { get; set; } = null!;

        public ICollection<Location> Locations { get; set; } = null!;

        public GeoCoordinates Coordinates { get; set; } = null!;

        public ICollection<EntityImage> Images { get; set; } = null!;

        public Country()
        {
        }

        public Country(string isoName, GeoCoordinates coordinates, ICollection<EntityImage>? images = null)
        {
            IsoName = isoName;
            Coordinates = coordinates;
            Images = images ?? new HashSet<EntityImage>();
        }
    }
}
