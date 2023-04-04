using IdentityModel.Client;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using TravelAgency.Application;

namespace TravelAgency.Auth;

public class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
{
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly Config _settings;

	public ConfigureSwaggerGenOptions(
		IOptions<Config> settings,
		IHttpClientFactory httpClientFactory)
	{
		_settings = settings.Value;
		_httpClientFactory = httpClientFactory;
	}

	public void Configure(SwaggerGenOptions options)
	{
		var discoveryDocument = GetDiscoveryDocument();

		options.OperationFilter<AuthorizeOperationFilter>();
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
						{ "TravelAgencyAPI", "API access" }
					}
				}
			},
			Description = "TravelAgency API access"
		});
	}

	private DiscoveryDocumentResponse GetDiscoveryDocument()
	{
		return _httpClientFactory
			.CreateClient()
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