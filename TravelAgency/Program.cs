using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using TravelAgency.Application;
using TravelAgency.Auth;
using TravelAgency.Domain.Entities;
using TravelAgency.Infrastructure.Data;

namespace TravelAgency
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.Configure<Config>(builder.Configuration)
				.AddScoped(sp => sp.GetRequiredService<IOptionsSnapshot<Config>>().Value);

			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
			builder.Services.AddDbContext<TravelAgencyDbContext>(options =>
				options.UseMySql(connectionString, new MySqlServerVersion(new Version())));

			builder.Services.AddDatabaseDeveloperPageExceptionFilter();

			builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
				.AddEntityFrameworkStores<TravelAgencyDbContext>()
				.AddDefaultUI()
				.AddDefaultTokenProviders();

			builder.Services
				.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();

			builder.Services.AddIdentityServer(options =>
			{
				if (!builder.Environment.IsDevelopment()) return;
				options.Events.RaiseErrorEvents = true;
				options.Events.RaiseInformationEvents = true;
				options.Events.RaiseFailureEvents = true;
				options.Events.RaiseSuccessEvents = true;
			}).AddApiAuthorization<ApplicationUser, TravelAgencyDbContext>(o =>
			{
				if (builder.Environment.IsDevelopment())
				{
					o.Clients[0].RedirectUris.Add("/swagger/oauth2-redirect.html");
				}
			});
			builder.Services.AddAuthentication();

			builder.Services.AddControllersWithViews();
			builder.Services.AddRazorPages();
			builder.Services.AddControllers();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			if (builder.Environment.IsDevelopment())
			{
				builder.Services.AddEndpointsApiExplorer();
				builder.Services.AddSwaggerGen();
			}

			builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

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
					setup.OAuthClientId("TravelAgency.Swagger");
					setup.OAuthAppName("TravelAgency.Swagger");
					setup.OAuthScopeSeparator(" ");
					setup.OAuthUsePkce();
				});
			}

			app.MapRazorPages();
			app.MapControllers();
			app.MapFallbackToFile("index.html");

			await app.RunAsync();
		}
	}
}