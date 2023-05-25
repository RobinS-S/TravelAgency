using System.Net;

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
                            var token = await AuthService.GetRefreshToken();
                            if (token == null)
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
    }
}
