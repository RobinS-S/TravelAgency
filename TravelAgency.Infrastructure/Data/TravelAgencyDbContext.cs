using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TravelAgency.Domain.Entities;

namespace TravelAgency.Infrastructure.Data
{
	public class TravelAgencyDbContext : ApiAuthorizationDbContext<ApplicationUser>
	{
		public TravelAgencyDbContext(
			DbContextOptions options,
			IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
		{
		}
	}
}
