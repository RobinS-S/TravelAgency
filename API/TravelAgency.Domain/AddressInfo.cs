namespace TravelAgency.Domain
{
    public class AddressInfo
    {
        public AddressInfo(string? country, string? city, string? province, string? postalCode, string? address)
        {
            Country = country;
            City = city;
            Province = province;
            PostalCode = postalCode;
            Address = address;
        }

        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public string? PostalCode { get; set; }
        public string? Address { get; set; }
    }
}
