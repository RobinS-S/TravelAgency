using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAgency.Domain.Entities;

namespace TravelAgency.Infrastructure.Data.Configuration
{
    public class ReservationEntityTypeConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasOne(r => r.Residence)
                .WithMany()
                .HasForeignKey(r => r.ResidenceId);

            builder.Property(r => r.Start)
                .IsRequired();

            builder.Property(r => r.End)
                .IsRequired();

            builder.Property(r => r.Cost)
                .IsRequired();
        }
    }
}
