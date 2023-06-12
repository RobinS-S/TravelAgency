using Azure.Storage;
using Azure.Storage.Blobs;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using TravelAgency.Application;
using TravelAgency.Application.Services;
using TravelAgency.Application.Services.Interfaces;
using TravelAgency.Auth;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Repositories.Interfaces;
using TravelAgency.Infrastructure.Data;
using TravelAgency.Infrastructure.Repositories;
using TravelAgency.Services;

namespace TravelAgency
{
    public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

            // Services
			builder.Services.Configure<Config>(builder.Configuration)
				.AddScoped(sp => sp.GetRequiredService<IOptionsSnapshot<Config>>().Value);

			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
			builder.Services.AddDbContext<TravelAgencyDbContext>(options =>
				options.UseMySql(connectionString, new MySqlServerVersion(new Version()), o => o.UseNetTopologySuite()));

			builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services
                .AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<TravelAgencyDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

			builder.Services
				.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddControllers();

            builder.Services.AddAuthentication(options =>
            {
            })
                .AddJwtBearer(options =>
                {
                    options.Authority = builder.Configuration["AppUrl"];
                    options.TokenValidationParameters.ValidateAudience = false;

                    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                });

            builder.Services.AddIdentityServer(options =>
			{
				if (!builder.Environment.IsDevelopment()) return;
				options.Events.RaiseErrorEvents = true;
				options.Events.RaiseInformationEvents = true;
				options.Events.RaiseFailureEvents = true;
				options.Events.RaiseSuccessEvents = true;
                options.UserInteraction.LoginUrl = "/Identity/Account/Login";
                options.UserInteraction.AllowOriginInReturnUrl = true;
                options.UserInteraction.LogoutUrl = "/Identity/Account/Logout";
                options.UserInteraction.ErrorUrl = "/Identity/Account/Error";
            })
			.AddInMemoryIdentityResources(IdentityConfig.IdentityResources)
			.AddInMemoryApiScopes(IdentityConfig.ApiScopes)
			.AddInMemoryClients(IdentityConfig.Clients)
			.AddDeveloperSigningCredential()
			.AddJwtBearerClientAuthentication()
			.AddAspNetIdentity<ApplicationUser>();

			builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddTransient<IRedirectUriValidator, TestAuthRedirectUriValidator>();

            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<IProfileService, ProfileService>();

            builder.Services.AddScoped<ICountryRepository, CountryRepository>();
            builder.Services.AddScoped<ILocationRepository, LocationRepository>();
            builder.Services.AddScoped<IResidenceRepository, ResidenceRepository>();
			builder.Services.AddScoped<IImageRepository, ImageRepository>();
            builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

            builder.Services.AddScoped<ReservationService>();

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddSwaggerGen();
                builder.Services.AddScoped<IImageService, LocalImageService>();
            }
            else
            {
                var azureStorageName = builder.Configuration.GetValue<string?>("AzureStorageName");
                var azureStorageKey = builder.Configuration.GetValue<string?>("AzureStorageKey");
                if (string.IsNullOrWhiteSpace(azureStorageKey) || string.IsNullOrWhiteSpace(azureStorageName))
                {
                    throw new Exception(
                        "You must specify AzureStorageName and AzureStorageKey in configuration as this is a production environment with no way of storing files.");
                }

                builder.Services.AddSingleton(GetBlobServiceClient(azureStorageName, azureStorageKey));
                builder.Services.AddScoped<IImageService, AzureBlobImageService>();
            }

            var app = builder.Build();

			app.UseHttpsRedirection();
			app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthorization();

			await DatabaseSeeder.SeedDatabase(app.Services); // Ensure the roles and a default user exists

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI(setup =>
				{
					setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Version 1.0");
					setup.OAuthClientId("TravelAgency.Client");
					setup.OAuthAppName("TravelAgency.Client");
					setup.OAuthScopeSeparator(" ");
					setup.OAuthUsePkce();
				});
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.MapRazorPages();
			app.MapControllers();
			app.MapFallbackToFile("index.html");

			await app.RunAsync();
		}

        public static BlobServiceClient GetBlobServiceClient(string accountName, string key)
        {
            var sharedKeyCredential = new StorageSharedKeyCredential(accountName, key);

            var client = new BlobServiceClient(
                new Uri($"https://{accountName}.blob.core.windows.net"),
                sharedKeyCredential);

            return client;
        }
    }
}