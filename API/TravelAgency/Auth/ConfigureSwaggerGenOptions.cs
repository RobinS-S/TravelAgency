using Duende.IdentityServer;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;
using System.Security.Authentication;
using TravelAgency.Application;

namespace TravelAgency.Auth;

public class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
{
	private readonly Config _settings;

	public ConfigureSwaggerGenOptions(
		IOptions<Config> settings)
	{
		_settings = settings.Value;
	}

	public void Configure(SwaggerGenOptions options)
	{
		var discoveryDocument = GetDiscoveryDocument();

		options.OperationFilter<AuthorizeOperationFilter>();
        options.OperationFilter<FormFileOperationFilter>();
        options.DescribeAllParametersInCamelCase();
		options.CustomSchemaIds(x => x.FullName);
		options.SwaggerDoc("v1", CreateOpenApiInfo());

        options.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
		{
			Type = SecuritySchemeType.OAuth2,
			Flows = new OpenApiOAuthFlows
			{
				AuthorizationCode = new OpenApiOAuthFlow
				{
					AuthorizationUrl = new Uri(discoveryDocument.AuthorizeEndpoint),
					TokenUrl = new Uri(discoveryDocument.TokenEndpoint),
					Scopes = new Dictionary<string, string>
					{
						{ "TravelAgencyAPI", "Customer API access" },
						{ IdentityServerConstants.StandardScopes.OpenId, "OpenID" },
                        { IdentityServerConstants.StandardScopes.Profile, "Profile" },
                    },
				},
			},
			Description = "TravelAgency API access"
		});
	}

	private DiscoveryDocumentResponse GetDiscoveryDocument()
	{
		var handler = new HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
            SslProtocols = SslProtocols.Tls12,
            ClientCertificateOptions = ClientCertificateOption.Manual
        };
        return new HttpClient(handler)
			.GetDiscoveryDocumentAsync(_settings.AppUrl)
			.GetAwaiter()
			.GetResult();
	}

	private static OpenApiInfo CreateOpenApiInfo()
	{
		return new OpenApiInfo
		{
			Title = "TravelAgency API",
			Version = "v1",
			Description = "Provides endpoints to exchange information with the TravelAgency database."
		};
	}
}
public class CustomHttpMessageHandler : HttpClientHandler
{
    public CustomHttpMessageHandler()
    {
        // Disable SSL certificate validation
        ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true;
    }
}