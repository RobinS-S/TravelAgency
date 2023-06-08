using Duende.IdentityServer.Models;
using Duende.IdentityServer;

namespace TravelAgency
{
    public class IdentityConfig
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

#pragma warning disable CA2211
        public static IEnumerable<ApiResource> ApiResources =
            new List<ApiResource>
            {
                new ApiResource("TravelAgencyAPI", "TravelAgencyAPI", new List<string>() { "TravelAgencyAPI" })
            };
#pragma warning restore CA2211

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope> {
                new ApiScope("TravelAgencyAPI", "TravelAgency API")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "TravelAgency.Client",
                    Enabled = true,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RedirectUris = { "/authentication/login-callback", "/swagger/oauth2-redirect.html", "travelagency://callback"},
                    PostLogoutRedirectUris = { "/authentication/logout-callback" },
                    AllowOfflineAccess = true,
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "TravelAgencyAPI"
                    },
                }
            };
    }
}