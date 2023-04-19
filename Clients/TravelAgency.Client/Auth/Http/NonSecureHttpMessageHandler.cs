namespace TravelAgency.Client.Auth.Http;

public class NonSecureHttpMessageHandler : HttpClientHandler
{
    public NonSecureHttpMessageHandler()
    {
        // Disable SSL certificate validation
        ServerCertificateCustomValidationCallback = (_, _, _, _) => true;
    }
}