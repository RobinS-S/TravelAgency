using TravelAgency.Client.Auth.Services;

namespace TravelAgency.Client.Auth.Pages
{
    public class AuthenticatedContentPage : ContentPage
    {
        private readonly AuthService _authService;
        private bool _isTokenCheckPerformed;

        public AuthenticatedContentPage(AuthService authService)
        {
            _authService = authService;
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            StartLogin();
            _isTokenCheckPerformed = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!_isTokenCheckPerformed)
            {
                Content.IsVisible = false;

                StartLogin();

                _isTokenCheckPerformed = true;
            }
            else
            {
                if (!Content.IsVisible)
                {
                    Content.IsVisible = true;
                }
            }
        }

        protected override void OnDisappearing()
        {
            _isTokenCheckPerformed = false;
        }

        private async Task StartLogin()
        {
            var hasToken = await _authService.LoadTokenFromStorage();
            if (!hasToken)
            {
                var shouldStartLoginProcess = _authService.CanStartLoginProcess();
                if (shouldStartLoginProcess)
                {
                    Dispatcher.Dispatch(OpenLoginPage);
                }
            }
            else
            {
                Content.IsVisible = true;
            }
        }

        private async void OpenLoginPage()
        {
            await Shell.Current.GoToAsync("///login");
        }
    }
}
