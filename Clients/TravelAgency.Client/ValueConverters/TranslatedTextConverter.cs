using System.Globalization;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.ValueConverters
{
    public class TranslatedTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ICollection<TranslatedTextDto> list)
            {
                var text = list.SingleOrDefault(t => t.IsoLanguageName == culture.TwoLetterISOLanguageName) ??
                    list.SingleOrDefault(t => t.IsoLanguageName == "en");

                if (text != null)
                {
                    return text.Text;
                }
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
