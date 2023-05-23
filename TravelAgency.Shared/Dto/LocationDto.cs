using TravelAgency.Shared.Enum;

namespace TravelAgency.Shared.Dto
{
    public class LocationDto
    {
        public long? Id { get; set; }
        public ICollection<TranslatedTextDto> Names { get; set; } = null!;
        public ICollection<TranslatedTextDto> Descriptions { get; set; } = null!;
        public LocationType LocationType { get; set; }
        public long CountryId { get; set; }
        public GeoCoordinatesDto Coordinates { get; set; } = null!;
        public ICollection<EntityImageDto> Images { get; set; } = null!;
    }

}
