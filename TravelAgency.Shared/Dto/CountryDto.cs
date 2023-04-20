namespace TravelAgency.Shared.Dto
{
    public class CountryDto
    {
        public long? Id { get; set; }
        public string IsoName { get; set; } = null!;
        public GeoCoordinatesDto Coordinates { get; set; } = null!;
    }
}
