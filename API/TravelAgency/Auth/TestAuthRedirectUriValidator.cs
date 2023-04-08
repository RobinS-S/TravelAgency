using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using TravelAgency.Application;

namespace TravelAgency.Auth;

public class TestAuthRedirectUriValidator : IRedirectUriValidator
{
    private readonly Config _config;

    public TestAuthRedirectUriValidator(Config config)
    {
        _config = config;
    }

    public Task<bool> IsRedirectUriValidAsync(string requestedUri, Client client)
    {
        var redirectUrls = new[] { _config.AppUrl + "/swagger/oauth2-redirect.html", "travelagency://callback" };
        return Task.FromResult(redirectUrls.Contains(requestedUri));
    }

    public Task<bool> IsPostLogoutRedirectUriValidAsync(string requestedUri, Client client)
    {
        return Task.FromResult(true);
    }
}