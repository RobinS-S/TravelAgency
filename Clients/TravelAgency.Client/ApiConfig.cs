namespace TravelAgency.Client
{
    public class ApiConfig
    {
#if DEBUG
        public const string ApiUrl = "https://travelagencyavans.azurewebsites.net";//"https://travelagency.local:7070";
        public const string ApiTestAuthenticatedUrl = "https://travelagencyavans.azurewebsites.net/api/account/profile";//"https://travelagency.local:7070/api/account/profile";
        public const string ClientId = "TravelAgency.Client";
        public const string Scopes = "openid offline_access profile TravelAgencyAPI";
        public const string CustomProtocolRedirectUri = "travelagency://callback";
        public const string AppShellDefaultPageAfterLogin = "//account";
        public const string AppShellDefaultPageAnonymous = "//countries";
#else
        public const string ApiUrl = "https://travelagencyavans.azurewebsites.net";
        public const string ApiTestAuthenticatedUrl = "https://travelagencyavans.azurewebsites.net/api/account/profile";
        public const string ClientId = "TravelAgency.Client";
        public const string Scopes = "openid offline_access profile TravelAgencyAPI";
        public const string CustomProtocolRedirectUri = "travelagency://callback";
        public const string AppShellDefaultPageAfterLogin = "//account";
        public const string AppShellDefaultPageAnonymous = "//countries";
#endif
    }
}
