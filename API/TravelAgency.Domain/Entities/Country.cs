using TravelAgency.Domain.Helpers;

namespace TravelAgency.Domain.Entities
{
    public class Country : Entity
    {
        public string IsoName { get; set; } = null!;

        public ICollection<Location> Locations { get; set; } = null!;

        public GeoCoordinates Coordinates { get; set; } = null!;

        public ICollection<CountryImage> Images { get; set; } = null!;

        public Country()
        {
        }

        public Country(string isoName, GeoCoordinates coordinates, ICollection<CountryImage>? images = null)
        {
            IsoName = isoName;
            Coordinates = coordinates;
            Images = images ?? new HashSet<CountryImage>();
        }
    }
}
