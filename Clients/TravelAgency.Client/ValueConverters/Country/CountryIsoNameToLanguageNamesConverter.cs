using Nager.Country;
using Nager.Country.Translation;
using System.Globalization;
using TravelAgency.Shared.Internationalization;

namespace TravelAgency.Client.ValueConverters.Country
{
    public class CountryIsoNameToLanguageNamesConverter
        : IValueConverter
    {
        private readonly CountryProvider countryProvider;
        private readonly TranslationProvider countryTranslationProvider;

        public CountryIsoNameToLanguageNamesConverter()
        {
            countryProvider = ServiceProviderHelper.GetService<CountryProvider>()!;
            countryTranslationProvider = ServiceProviderHelper.GetService<TranslationProvider>()!;
        }

        public object? Convert(object val, Type targetType, object parameter, CultureInfo culture)
        {
            string isoName = (string)val;
            var info = countryProvider.GetCountry(isoName);
            if (info != null && SpokenLanguagesByIsoCountry.CountrySpokenLanguageCombinations.TryGetValue(isoName.ToUpper(), out var value))
            {
                var codes = value;
                var languages = codes.Select(countryTranslationProvider.GetLanguage);
                var names = languages.Select(lang => lang.Translations.SingleOrDefault(lt => lt.LanguageCode == Enum.Parse<LanguageCode>(culture.TwoLetterISOLanguageName.ToUpper()))?.Name ?? lang.CommonName);
                return string.Join(", ", names);
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

