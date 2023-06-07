namespace TravelAgency.Shared.Dto
{
    public class GeoCoordinatesDto
    {
        public GeoCoordinatesDto(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

}
