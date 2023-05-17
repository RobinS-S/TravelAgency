using System.Globalization;
using TravelAgency.Client.Localization;

namespace TravelAgency.Client.ValueConverters
{
    public class ThemeNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var labelConverter = new TranslateExtension();
            return labelConverter.Convert($"Theme.{value}", typeof(string), parameter, CultureInfo.CurrentUICulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
