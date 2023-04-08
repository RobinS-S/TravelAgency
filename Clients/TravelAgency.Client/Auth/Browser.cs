using IdentityModel.OidcClient.Browser;
using IBrowser = IdentityModel.OidcClient.Browser.IBrowser;

namespace TravelAgency.Client.Auth
{
    public class Browser : IBrowser
    {
        public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
        {
            var authResult = await WebAuthenticator.AuthenticateAsync(new Uri(options.StartUrl), new Uri(ApiConfig.CustomProtocolRedirectUri));

            return new BrowserResult()
            {
                Response = ParseAuthenticatorResult(authResult)
            };
        }

        private static string ParseAuthenticatorResult(WebAuthenticatorResult result)
        {
            var code = result?.Properties["code"];
            var scope = result?.Properties["scope"];
            var state = result?.Properties["state"];
            var sessionState = result?.Properties["session_state"];
            return $"{"travelagency://callback"}#code={code}&scope={scope}&state={state}&session_state={sessionState}";
        }
    }
}
