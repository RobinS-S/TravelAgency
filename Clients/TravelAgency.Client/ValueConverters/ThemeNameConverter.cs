using System.Globalization;
using TravelAgency.Client.Localization;

namespace TravelAgency.Client.ValueConverters
{
    public class ThemeNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return LocalizationResourceManager.Instance[$"Theme.{value}"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
