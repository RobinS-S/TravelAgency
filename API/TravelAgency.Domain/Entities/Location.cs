using TravelAgency.Domain.Helpers;
using TravelAgency.Shared.Enum;

namespace TravelAgency.Domain.Entities
{
    public class Location : Entity
    {
        public ICollection<TranslatedText> Names { get; set; } = null!;

        public ICollection<TranslatedText> Descriptions { get; set; } = null!;

        public LocationType LocationType { get; set; }

        public Country Country { get; set; } = null!;

        public long? CountryId { get; set; }

        public ICollection<Residence> Residences { get; set; } = null!;

        public GeoCoordinates Coordinates { get; set; } = null!;

        public Location() { }

        public Location(ICollection<TranslatedText> names, ICollection<TranslatedText> descriptions, LocationType locationType, Country country, GeoCoordinates coordinates)
        {
            Names = names;
            Descriptions = descriptions;
            LocationType = locationType;
            Country = country;
            Coordinates = coordinates;
        }
    }
}
