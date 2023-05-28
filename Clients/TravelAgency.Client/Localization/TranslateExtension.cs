using System.Globalization;

namespace TravelAgency.Client.Localization
{
    [ContentProperty(nameof(Name))]
    public class TranslateExtension : IMarkupExtension<BindingBase>, IValueConverter
    {
        public string? Name { get; set; }
        public string? StringFormat { get; set; } = "{0}";

        public BindingBase ProvideValue(IServiceProvider serviceProvider)
        {
            return new Binding
            {
                Mode = BindingMode.OneWay,
                Path = $"[{Name}]",
                Source = LocalizationResourceManager.Instance,
                Converter = this,
                ConverterParameter = StringFormat
            };
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string translatedValue && parameter is string format)
            {
                return string.Format(format, translatedValue);
            }

            return Name ?? "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
