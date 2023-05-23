using TravelAgency.Domain.Helpers;
using TravelAgency.Domain.Interfaces;

namespace TravelAgency.Domain.Entities
{
    public class Residence : Entity, IImageOwningEntity, IGeolocationOwningEntity
    {
        public ICollection<TranslatedText> Names { get; set; } = null!;

        public ICollection<TranslatedText> Descriptions { get; set; } = null!;

        public Location Location { get; set; } = null!;

        public long LocationId { get; set; }

        public GeoCoordinates Coordinates { get; set; } = null!;

        public ICollection<EntityImage> Images { get; set; } = null!;

        public Residence() { }

        public Residence(ICollection<TranslatedText> names, ICollection<TranslatedText> descriptions, Location location, GeoCoordinates coordinates, ICollection<EntityImage>? images = null)
        {
            Names = names;
            Descriptions = descriptions;
            Location = location;
            Coordinates = coordinates;
            Images = images ?? new HashSet<EntityImage>();
        }
    }
}
