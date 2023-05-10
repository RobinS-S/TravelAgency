using TravelAgency.Domain.Helpers;

namespace TravelAgency.Domain.Entities
{
    public class Residence : Entity
    {
        public ICollection<TranslatedText> Names { get; set; } = null!;

        public ICollection<TranslatedText> Descriptions { get; set; } = null!;

        public Location Location { get; set; } = null!;

        public long LocationId { get; set; }

        public GeoCoordinates? Coordinates { get; set; }

        public Residence() { }

        public Residence(ICollection<TranslatedText> names, ICollection<TranslatedText> descriptions, Location location, GeoCoordinates? coordinates = null)
        {
            Names = names;
            Descriptions = descriptions;
            Location = location;
            Coordinates = coordinates;
        }
    }
}
