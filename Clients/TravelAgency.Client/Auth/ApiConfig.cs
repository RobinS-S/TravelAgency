namespace TravelAgency.Client.Auth
{
    public class ApiConfig
    {
    #if DEBUG
        public const string ApiUrl = "https://travelagency.local:7070";
        public const string ApiTestAuthenticatedUrl = "https://travelagency.local:7070/api/account/profile";
        public const string ClientId = "TravelAgency.Client";
        public const string Scopes = "openid offline_access profile TravelAgencyAPI";
        public const string CustomProtocolRedirectUri = "travelagency://callback";
        public const string AppShellDefaultPageAfterLogin = "//MainPage";
    #else
    #endif
    }
}
