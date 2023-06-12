using System.Globalization;
using TravelAgency.Client.Localization;
using TravelAgency.Shared.Enum;

namespace TravelAgency.Client.ValueConverters.Location
{
    public class LocationTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is LocationType)
            {
                return LocalizationResourceManager.Instance[$"LocationType.{value}"];
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
