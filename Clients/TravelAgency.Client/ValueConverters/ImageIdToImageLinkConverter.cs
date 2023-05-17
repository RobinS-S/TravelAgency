using System.Globalization;

namespace TravelAgency.Client.ValueConverters
{
    public class ImageIdToImageLinkConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"{ApiConfig.ApiUrl}/api/Images/{(long)value}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
