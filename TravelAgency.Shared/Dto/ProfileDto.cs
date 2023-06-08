namespace TravelAgency.Shared.Dto
{
    public class ProfileDto
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public bool HasWhatsApp { get; set; }
    }
}