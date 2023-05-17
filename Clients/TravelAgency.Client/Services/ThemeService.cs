using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace TravelAgency.Client.Services
{
    public partial class ThemeService : ObservableObject
    {
        private const string ThemeKey = "theme";

        [ObservableProperty]
        public List<AppTheme> _themes;

        [ObservableProperty]
        public AppTheme _theme = AppTheme.Unspecified;

        public ThemeService()
        {
            _themes = Enum.GetValues(typeof(AppTheme)).Cast<AppTheme>().ToList();
            LoadTheme();
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

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            
            if (e.PropertyName == nameof(Theme))
            {
                SaveTheme();
            }
        }
    }
}
