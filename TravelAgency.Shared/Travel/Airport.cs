using TravelAgency.Shared.Dto;

namespace TravelAgency.Shared.Travel
{
    public class Airport
    {
        public string Location { get; set; }
        public string Name { get; set; }
        public string IATACode { get; set; }
        public string Country { get; }
        public GeoCoordinatesDto GeoCoordinates { get; set; }

        public Airport(string location, string name, string iataCode, string country, GeoCoordinatesDto geoCoordinates)
        {
            Location = location;
            Name = name;
            IATACode = iataCode;
            Country = country;
            GeoCoordinates = geoCoordinates;
        }
    }
}
