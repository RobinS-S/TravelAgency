using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAgency.Domain.Entities;
using TravelAgency.Infrastructure.Helpers;

namespace TravelAgency.Infrastructure.Data.Configuration
{
    public class ReservationEntityTypeConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasOne(r => r.Residence)
                .WithMany()
                .HasForeignKey(r => r.ResidenceId);

            builder.HasOne(r => r.Residence)
                .WithMany()
                .HasForeignKey(r => r.ResidenceId);

            builder.Property(r => r.Start)
                .IsRequired();

            builder.Property(r => r.End)
                .IsRequired();

            builder.Property(r => r.Cost)
                .IsRequired();

            builder.HasOne(r => r.Tenant)
                .WithMany();

            builder.OwnsMany(r => r.Flights, builder =>
            {
                builder.Property(c => c.Coordinates)
                    .UseGeoCoordinateConversion();

                builder.HasOne(i => i.Owner)
                    .WithMany()
                    .HasForeignKey(i => i.OwnerId);

                builder.OwnsMany(f => f.Seats, builder =>
                {
                    builder.Property(s => s.SeatNumber)
                        .HasMaxLength(8);

                    builder.WithOwner();
                });

                builder.Property(f => f.AirportCode)
                    .HasMaxLength(32);

                builder.Property(f => f.FlightNumber)
                    .HasMaxLength(32);

                builder.WithOwner();
            });
        }
    }
}
