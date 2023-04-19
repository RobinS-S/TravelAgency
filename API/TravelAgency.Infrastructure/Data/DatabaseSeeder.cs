using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TravelAgency.Application;
using TravelAgency.Domain.Entities;

namespace TravelAgency.Infrastructure.Data
{
	public class DatabaseSeeder
	{
		public static async Task SeedDatabase(IServiceProvider serviceProvider)
		{
			using var scope = serviceProvider.CreateScope();
			var dbContext = scope.ServiceProvider.GetRequiredService<TravelAgencyDbContext>();
			var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
			var configuration = scope.ServiceProvider.GetRequiredService<Config>();
			var adminRoleName = Roles.AdminRoleName;

			var roleExist = await roleManager.RoleExistsAsync(adminRoleName);
			if (!roleExist) await roleManager.CreateAsync(new IdentityRole(adminRoleName));

			if (string.IsNullOrWhiteSpace(configuration.AdminUserEmail) || string.IsNullOrWhiteSpace(configuration.AdminUserPassword))
				throw new Exception(
					"You need to provide a default user account which will be created with the Admin role, keys: AppSettings:AdminUserEmail and AppSettings:AdminUserPassword");

			var defaultUser = new ApplicationUser
			{
				UserName = configuration.AdminUserEmail,
				Email = configuration.AdminUserEmail
			};

			var user = await userManager.FindByEmailAsync(configuration.AdminUserEmail);

			if (user == null)
			{
				var createPowerUser = await userManager.CreateAsync(defaultUser, configuration.AdminUserPassword);
				if (createPowerUser.Succeeded)
				{
					await userManager.AddToRoleAsync(defaultUser, Roles.AdminRoleName);
					user = await userManager.FindByEmailAsync(configuration.AdminUserEmail);
				}
			}
			else
			{
				var isAdmin = await userManager.IsInRoleAsync(user, Roles.AdminRoleName);
				if (!isAdmin) await userManager.AddToRoleAsync(user, Roles.AdminRoleName);
			}

			if (user != null)
			{
				await SeedSampleData(dbContext, user);
			}
		}

		private static async Task SeedSampleData(TravelAgencyDbContext dbContext, ApplicationUser user)
        {
            await Task.Delay(0);
        }
	}
}
