using TravelAgency.Client.Auth;
using TravelAgency.Client.Auth.Services;

namespace TravelAgency.Client.Pages.Main
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpService _httpService;
        private readonly AuthService _authService;

        public MainPage(HttpService httpService, AuthService authService)
        {
            _httpService = httpService;
            _authService = authService;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            CounterBtn.Text = "Click me";
            TokenLabel.Text = "Welcome to .NET Multi-platform App UI";
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            SemanticScreenReader.Announce(CounterBtn.Text);

            var response = await _httpService.GetResponseAsync(new Uri(ApiConfig.ApiUrl + "/api/Account/profile"));
            if (response != null)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                CounterBtn.Text = response.StatusCode.ToString();
                TokenLabel.Text = responseText;
            }
            else
            {
                CounterBtn.Text = "NOT LOGGED IN";
                TokenLabel.Text = "";
            }
        }

        private void OnRemoveLoginClicked(object sender, EventArgs e)
        {
            _authService.ResetCredentials();
        }
    }
}