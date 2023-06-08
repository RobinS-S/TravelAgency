using System.Globalization;
using TravelAgency.Shared.Airports;

namespace TravelAgency.Client.ValueConverters
{
    public class AirportIATACodeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string code)
            {
                return AirTravelInformation.GetAirportByIATACode(code)?.Name ?? "?";
            }

            return "?";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
