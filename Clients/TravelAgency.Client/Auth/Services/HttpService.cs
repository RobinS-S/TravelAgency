using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace TravelAgency.Client.Auth.Services
{
    public class HttpService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;

        public HttpService(HttpClient httpClient, AuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> TestLogin()
        {
            try
            {
                var response = await _httpClient.GetAsync(new Uri(ApiConfig.ApiTestAuthenticatedUrl));
                if (response.StatusCode is not (HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden)) return true;

                _authService.ResetCredentials();
                return false;

            }
            catch
            {
            }
            return false;
        }

        public async Task<HttpResponseMessage?> GetResponseAsync(Uri uri, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _httpClient.GetAsync(uri, cancellationToken);
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        {
                            var token = await _authService.GetRefreshToken();
                            if (string.IsNullOrEmpty(token))
                            {
                                await _authService.StartLoginProcess();
                                return null;
                            }

                            var refreshed = await _authService.RefreshToken();
                            if (!refreshed)
                            {
                                await _authService.StartLoginProcess();
                                return null;
                            }

                            response = await _httpClient.GetAsync(uri, cancellationToken);
                            if (response.StatusCode == HttpStatusCode.Unauthorized)
                            {
                                await _authService.StartLoginProcess();
                                return null;
                            }
                            break;
                        }
                }

                return response;
            }
            catch
            {
                return null;
            }
        }

        public async Task<HttpResponseMessage?> PostAsJsonAsync<TValue>([StringSyntax(StringSyntaxAttribute.Uri)] string? requestUri, TValue value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(requestUri, value, options, cancellationToken);
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        {
                            var token = await _authService.GetRefreshToken();
                            if (string.IsNullOrEmpty(token))
                            {
                                await _authService.StartLoginProcess();
                                return null;
                            }

                            var refreshed = await _authService.RefreshToken();
                            if (!refreshed)
                            {
                                await _authService.StartLoginProcess();
                                return null;
                            }

                            response = await _httpClient.PostAsJsonAsync(requestUri, value, options, cancellationToken);
                            if (response.StatusCode == HttpStatusCode.Unauthorized)
                            {
                                await _authService.StartLoginProcess();
                                return null;
                            }
                            break;
                        }
                }

                return response;
            }
            catch
            {
                return null;
            }
        }
    }
}
