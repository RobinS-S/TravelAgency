using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAgency.Domain.Entities;

namespace TravelAgency.Infrastructure.Data.Configuration
{
    public class LocationEntityTypeConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.OwnsMany(l => l.Names, b =>
            {
                b.Property(n => n.IsoLanguageName).HasMaxLength(3);
                b.Property(n => n.Text).HasMaxLength(128);
            });

            builder.OwnsMany(l => l.Descriptions, b =>
            {
                b.Property(n => n.IsoLanguageName).HasMaxLength(3);
                b.Property(n => n.Text).HasMaxLength(256);
            });

            builder.Property(l => l.LocationType)
                .IsRequired();

            builder.HasMany(l => l.Residences)
                .WithOne(c => c.Location);
        }
    }
}
