using System.ComponentModel;
using System.Globalization;
using TravelAgency.Client.Resources.Localization;

namespace TravelAgency.Client.Localization
{
    public class LocalizationResourceManager : INotifyPropertyChanged
    {
        private LocalizationResourceManager()
        {
            Translations.Culture = CultureInfo.CurrentCulture;
        }

        public static LocalizationResourceManager Instance { get; } = new();

        public object this[string resourceKey] => Translations.ResourceManager.GetObject(resourceKey, Translations.Culture) ?? Array.Empty<byte>();

        public event PropertyChangedEventHandler? PropertyChanged;

        public void SetCulture(CultureInfo culture)
        {
            if(Translations.Culture == culture)
            {
                return;
            }

            Translations.Culture = culture;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
