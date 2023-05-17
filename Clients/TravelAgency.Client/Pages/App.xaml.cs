using System.Globalization;
using TravelAgency.Client.Localization;
using TravelAgency.Client.Services;

namespace TravelAgency.Client.Pages
{
    public partial class App : Application
    {
        private SettingsService _settingsService;

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            _settingsService = ServiceProviderHelper.GetService<SettingsService>()!;
            _settingsService.PropertyChanged += _SettingsService_PropertyChanged;

            SetTheme();
            SetLanguage();
        }

        private void _SettingsService_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_settingsService.Theme))
            {
                SetTheme();
            }
            else if (e.PropertyName == nameof(_settingsService.Language))
            {
                SetLanguage();
            }
        }

        private void SetTheme()
        {
            UserAppTheme = _settingsService.Theme;
        }

        private void SetLanguage()
        {
            LocalizationResourceManager.Instance.SetCulture(new CultureInfo(_settingsService.Language));
        }
    }
}