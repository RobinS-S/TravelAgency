using Nager.Country.Translation;
using System.Globalization;

namespace TravelAgency.Client.ValueConverters
{
    public class LanguageNameConverter : IValueConverter
    {
        private readonly TranslationProvider countryTranslationProvider;

        public LanguageNameConverter()
        {
            countryTranslationProvider = ServiceProviderHelper.GetService<TranslationProvider>()!;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var country = countryTranslationProvider.GetLanguage((string)value);
            if(country == null)
            {
                return "";
            }

            return country.OfficialName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
