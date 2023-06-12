using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Results;
using System.Net.Http.Headers;
using TravelAgency.Client.Auth.Pages;

namespace TravelAgency.Client.Auth.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly OidcClient _openIdClient;
        private bool _loggingIn;
        private bool _hasAuthToken;
        public event EventHandler<bool>? AuthStatusChanged;
        public const string AuthTokenStorageKey = "authToken";
        public const string RefreshTokenStorageKey = "refreshToken";

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _openIdClient = new OidcClient(new OidcClientOptions
            {
                Authority = ApiConfig.ApiUrl,
                ClientId = ApiConfig.ClientId,
                Scope = ApiConfig.Scopes,
                RedirectUri = ApiConfig.CustomProtocolRedirectUri,
                HttpClientFactory = _ => _httpClient
            });
        }

        public bool HasAuthToken => _hasAuthToken;

        public async Task StartLoginProcess()
        {
            if (_loggingIn || Shell.Current.CurrentState.Location.ToString() == "//login")
            {
                return;
            }

            _loggingIn = true;
            var loginPage = ServiceProviderHelper.GetService<LoginPage>();
            await Shell.Current.Navigation.PushAsync(loginPage, true);
        }

        public bool CanStartLoginProcess()
        {
            if (_loggingIn || Shell.Current.CurrentState.Location.ToString() == "//login")
            {
                return false;
            }

            _loggingIn = true;
            return true;
        }

        public async Task SetAuthResult(LoginResult result)
        {
            await HandleResult(result.IsError, result.AccessToken, result.RefreshToken);
        }

        private async Task SetAuthResult(RefreshTokenResult result)
        {
            await HandleResult(result.IsError, result.AccessToken, result.RefreshToken);
        }

        public async Task<bool> LoadTokenFromStorage()
        {
            var token = await GetAuthToken();
            LoadToken(token);
            return !string.IsNullOrEmpty(token);
        }

        public void LoadToken(string? accessToken)
        {
            _httpClient.DefaultRequestHeaders.Remove("Authorization");
            _httpClient.DefaultRequestHeaders.Authorization = accessToken == null ? null : new AuthenticationHeaderValue("Bearer", accessToken);
            var hasToken = !string.IsNullOrEmpty(accessToken);
            if (_hasAuthToken == hasToken) return;
            _hasAuthToken = hasToken;
            this.AuthStatusChanged?.Invoke(this, _hasAuthToken);
        }

        private async Task HandleResult(bool isError, string accessToken, string refreshToken)
        {
            if (!isError)
            {
                LoadToken(accessToken);
                await SecureStorage.Default.SetAsync(AuthTokenStorageKey, accessToken);
                if (!string.IsNullOrEmpty(accessToken))
                {
                    await SecureStorage.Default.SetAsync(RefreshTokenStorageKey, refreshToken);
                }
            }
            else
            {
                ResetCredentials();
            }
        }

        public void ResetCredentials()
        {
            SecureStorage.Default?.Remove(AuthTokenStorageKey);
            SecureStorage.Default?.Remove(RefreshTokenStorageKey);
            LoadToken(null);
            _hasAuthToken = false;
        }

        public async Task<string> GetAuthToken() => await SecureStorage.Default.GetAsync(AuthTokenStorageKey);
        public async Task<string> GetRefreshToken() => await SecureStorage.Default.GetAsync(RefreshTokenStorageKey);

        public async Task<bool> RefreshToken()
        {
            var refreshToken = await GetRefreshToken();
            if (string.IsNullOrEmpty(refreshToken))
            {
                return false;
            }

            var result = await _openIdClient.RefreshTokenAsync(refreshToken);
            await SetAuthResult(result);
            return !result.IsError;
        }

        public async Task<LoginResult> Login(IdentityModel.OidcClient.Browser.IBrowser browser)
        {
            _loggingIn = true;
            _openIdClient.Options.Browser = browser;
            var loginResult = await _openIdClient.LoginAsync(new LoginRequest());
            await SetAuthResult(loginResult);

            if (!loginResult.IsError)
            {
                _loggingIn = false;
            }
            else
            {
                Console.WriteLine("ERROR: {0}", loginResult.Error);
            }
            return loginResult;
        }

        public async Task<LogoutResult> Logout(IdentityModel.OidcClient.Browser.IBrowser browser)
        {
            _loggingIn = true;
            _openIdClient.Options.Browser = browser;
            var logoutResult = await _openIdClient.LogoutAsync(new LogoutRequest());

            if (logoutResult.IsError)
            {
                Console.WriteLine("ERROR: {0}", logoutResult.Error);
            }

            ResetCredentials();
            _loggingIn = false;
            return logoutResult;
        }
    }
}
