using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Globalization;

namespace TravelAgency.Client.Services
{
    public partial class SettingsService : ObservableObject
    {
        private const string ThemeKey = "theme";
        private const string LanguageKey = "language";

        [ObservableProperty]
        public List<AppTheme> _themes;

        [ObservableProperty]
        public AppTheme _theme = AppTheme.Unspecified;

        [ObservableProperty]
        public List<string> _languages = new() { "nl", "en" };

        [ObservableProperty]
        public string _language = "en";

        public SettingsService()
        {
            _themes = Enum.GetValues(typeof(AppTheme)).Cast<AppTheme>().ToList();

            LoadTheme();
            LoadLanguage();
        }

        private void LoadTheme()
        {
            var loadedTheme = Preferences.Get(ThemeKey, 0);
            Theme = (AppTheme)loadedTheme;
        }

        private void SaveTheme()
        {
            Preferences.Set(ThemeKey, (int)Theme);
        }

        private void LoadLanguage()
        {
            var loadedLanguage = Preferences.Get(LanguageKey, null);
            if (loadedLanguage == null)
            {
                switch (CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
                {
                    case "en":
                    case "nl":
                        loadedLanguage = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
                        break;
                    default:
                        loadedLanguage = "en";
                        break;
                }
            }
            Language = loadedLanguage;
        }

        private void SaveLanguage()
        {
            var culture = new CultureInfo(Language);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            Preferences.Set(LanguageKey, Language);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.PropertyName == nameof(Theme))
            {
                SaveTheme();
            }
            else if (e.PropertyName == nameof(Language))
            {
                SaveLanguage();
            }
        }
    }
}
