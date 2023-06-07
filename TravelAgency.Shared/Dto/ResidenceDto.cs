namespace TravelAgency.Shared.Dto
{
    public class ResidenceDto
    {
        public long? Id { get; set; }
        public ICollection<TranslatedTextDto> Names { get; set; } = null!;
        public ICollection<TranslatedTextDto> Descriptions { get; set; } = null!;
        public long? LocationId { get; set; }
        public GeoCoordinatesDto Coordinates { get; set; } = null!;
        public ICollection<EntityImageDto> Images { get; set; } = null!;
        public int SuitableFor { get; set; }
        public decimal PricePerDay { get; set; }
    }
}
