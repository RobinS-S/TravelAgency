using Nager.Country.Translation;
using System.Globalization;

namespace TravelAgency.Client.ValueConverters.Country
{
    public class CountryIsoNameToCountryNameConverter : IValueConverter
    {
        private readonly TranslationProvider countryTranslationProvider;

        public CountryIsoNameToCountryNameConverter()
        {
            countryTranslationProvider = ServiceProviderHelper.GetService<TranslationProvider>()!;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string isoName = (string)value;
            string countryName = countryTranslationProvider.GetCountryTranslatedName(isoName, culture);
            return countryName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
