using TravelAgency.Domain.Helpers;

namespace TravelAgency.Domain.Entities
{
    public class Country : Entity
    {
        public string IsoName { get; set; } = null!;

        public ICollection<Location> Locations { get; set; } = null!;

        public GeoCoordinates Coordinates { get; set; } = null!;

        public Country()
        {
        }

        public Country(string isoName, GeoCoordinates coordinates)
        {
            IsoName = isoName;
            Coordinates = coordinates;
        }
    }
}
