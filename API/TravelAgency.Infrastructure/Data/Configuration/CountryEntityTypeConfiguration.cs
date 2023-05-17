﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAgency.Domain.Entities;
using TravelAgency.Infrastructure.Helpers;

namespace TravelAgency.Infrastructure.Data.Configuration
{
    public class CountryEntityTypeConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.Property(c => c.IsoName)
                .HasMaxLength(3);

            builder.HasMany(c => c.Locations)
                .WithOne(l => l.Country);

            builder.Property(c => c.Coordinates)
                .UseGeoCoordinateConversion();

            builder.OwnsMany(c => c.Images, b =>
            {
                b.HasOne<Image>()
                    .WithMany()
                    .IsRequired(true)
                    .HasForeignKey(c => c.ImageId);

                b.WithOwner();
            });
        }
    }
}