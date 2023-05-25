using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Reflection.Emit;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;
using TravelAgency.Infrastructure.Data.Configuration.Generic;

namespace TravelAgency.Infrastructure.Data
{
	public class TravelAgencyDbContext : ApiAuthorizationDbContext<ApplicationUser>
	{
		public DbSet<Country> Countries { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Residence> Residences { get; set; }

        public DbSet<Image> Images { get; set; }

        public TravelAgencyDbContext(
			DbContextOptions options,
			IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
            var entityTypes = builder.Model.GetEntityTypes().ToList();
            foreach (var entityType in entityTypes)
            {
                builder.ApplyConfiguration<IImageOwningEntity>(typeof(ImageOwningEntityTypeConfiguration<>), entityType.ClrType);
                builder.ApplyConfiguration<IGeolocationOwningEntity>(typeof(GeolocationOwningEntityTypeConfiguration<>), entityType.ClrType);
            }

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
