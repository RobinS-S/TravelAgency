using TravelAgency.Client.Services;

namespace TravelAgency.Client.Pages
{
    public partial class App : Application
    {
        private ThemeService _themeService;

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            _themeService = ServiceProviderHelper.GetService<ThemeService>()!;
            _themeService.PropertyChanged += _themeService_PropertyChanged;

            SetTheme();
        }

        private void _themeService_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_themeService.Theme))
            {
                SetTheme();
            }
        }

        private void SetTheme()
        {
            UserAppTheme = _themeService.Theme;
        }
    }
}