namespace TravelAgency.Domain.Entities
{
    public class TranslatedText : Entity
    {
        public string IsoLanguageName { get; set; } = null!;

        public string Text { get; set; } = null!;

        public TranslatedText() 
        {
        }

        public TranslatedText(string isoLanguageName, string text)
        {
            IsoLanguageName = isoLanguageName;
            Text = text;
        }
    }
}
