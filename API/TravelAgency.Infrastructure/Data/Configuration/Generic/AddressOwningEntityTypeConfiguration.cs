using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAgency.Domain.Interfaces;

namespace TravelAgency.Infrastructure.Data.Configuration.Generic
{
    internal class AddressOwningEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IAddressOwningEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.OwnsOne(ae => ae.Address, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.Property(c => c.Country)
                    .HasMaxLength(128);
                ownedNavigationBuilder.Property(c => c.City)
                    .HasMaxLength(128);
                ownedNavigationBuilder.Property(c => c.Province)
                    .HasMaxLength(128);
                ownedNavigationBuilder.Property(c => c.PostalCode)
                    .HasMaxLength(64);
                ownedNavigationBuilder.Property(c => c.Address)
                    .HasMaxLength(512);
            });
        }
    }
}
