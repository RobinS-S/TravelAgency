namespace TravelAgency.Domain.Interfaces
{
    public interface IAddressOwningEntity
    {
        public AddressInfo? Address { get; set; }
    }
}
