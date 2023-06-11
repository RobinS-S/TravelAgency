namespace TravelAgency.Shared.Dto
{
    public class AddressInfoDto
    {
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public string? PostalCode { get; set; }
        public string? Address { get; set; }

        public override string ToString()
        {
            var addressComponents = new List<string?> { Address, City, Province, PostalCode, Country };
            return string.Join(", ", addressComponents.Where(x => !string.IsNullOrEmpty(x)));
        }
    }
}
