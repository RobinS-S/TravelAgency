using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAgency.Domain.Interfaces;
using TravelAgency.Infrastructure.Helpers;

namespace TravelAgency.Infrastructure.Data.Configuration.Generic
{
    internal class GeolocationOwningEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IGeolocationOwningEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(c => c.Coordinates)
                .UseGeoCoordinateConversion();
        }
    }
}
