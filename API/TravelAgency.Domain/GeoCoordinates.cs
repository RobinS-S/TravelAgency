namespace TravelAgency.Domain
{
    public class GeoCoordinates
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public GeoCoordinates(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
