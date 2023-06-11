using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;
using TravelAgency.Infrastructure.Data.Configuration.Generic;

namespace TravelAgency.Infrastructure.Data
{
    public class TravelAgencyDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public DbSet<Country> Countries { get; set; } = null!;

        public DbSet<Location> Locations { get; set; } = null!;

        public DbSet<Residence> Residences { get; set; } = null!;

        public DbSet<Image> Images { get; set; } = null!;

        public DbSet<Reservation> Reservations { get; set; } = null!;

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
                builder.ApplyConfiguration<IUserOwnedEntity>(typeof(UserOwnedEntityTypeConfiguration<>), entityType.ClrType);
                builder.ApplyConfiguration<IAddressOwningEntity>(typeof(AddressOwningEntityTypeConfiguration<>), entityType.ClrType);
            }

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
