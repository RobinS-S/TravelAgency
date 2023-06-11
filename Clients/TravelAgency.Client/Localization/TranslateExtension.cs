using System.Diagnostics;
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
            var format = parameter as string ?? StringFormat ?? "{0}";
            if (value is string translatedValue)
            {
                try
                {
                    return string.Format(format, translatedValue);
                }
                catch (FormatException)
                {
                    Debug.WriteLine($"Invalid string format: {format}");
                }
            }

            return $"!{string.Format(format, Name)}!" ?? "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
