namespace TravelAgency.Shared.Dto
{
    public class TranslatedTextDto
    {
        public long? Id { get; set; }
        public string IsoLanguageName { get; set; } = null!;
        public string Text { get; set; } = null!;
    }

}
